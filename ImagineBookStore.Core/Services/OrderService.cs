using ImagineBookStore.Core.Extensions;
using ImagineBookStore.Core.Interfaces;
using ImagineBookStore.Core.Models.App;
using ImagineBookStore.Core.Models.Configurations;
using ImagineBookStore.Core.Models.Input;
using ImagineBookStore.Core.Models.Paystack;
using ImagineBookStore.Core.Models.Utilities;
using ImagineBookStore.Core.Models.View;
using ImagineBookStore.Core.Utilities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace ImagineBookStore.Core.Services;

public class OrderService : IOrderService
{
    private readonly BookStoreContext _context;
    private readonly HttpClient _httpClient;
    private readonly UserSession _userSession;

    public OrderService(BookStoreContext context, UserSession userSession, IHttpClientFactory httpClientFactory, IOptions<PasystackConfig> paystackConfig)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));

        ArgumentException.ThrowIfNullOrEmpty(nameof(httpClientFactory));
        _httpClient = httpClientFactory.CreateClient(paystackConfig.Value.HttpClientName);
    }

    public async Task<Result> AttemptPayment(int orderId)
    {
        var paymentData = await _context.Orders
            .Where(x => x.Id == orderId)
            .ProjectToType<PaymentRequestView>()
            .FirstOrDefaultAsync();

        if (paymentData == null) return new ErrorResult("Invalid order.");
        if (paymentData.IsPaid) return new ErrorResult("Payment has been completed.");

        return new SuccessResult(paymentData);
    }

    public async Task<Result> ConfirmPayment(int orderId)
    {
        var order = await _context.Orders
            .Include(x => x.Items).ThenInclude(ix => ix.Book)
            .Where(x => x.Id == orderId)
            .FirstOrDefaultAsync();

        if (order == null) return new ErrorResult("Invalid order.");

        if (order.IsPaid) return new ErrorResult("Order fulfilled.");

        var verifyTransaction = await VerifyTransaction(order.Reference);

        if (!verifyTransaction.status) return new ErrorResult(verifyTransaction.message);
        if (verifyTransaction.data.status != "success") return new ErrorResult("Payment not completed");

        order.IsPaid = true;

        foreach (var item in order.Items)
        {
            item.Book.TotalStock -= item.Quantity;
        }

        await _context.SaveChangesAsync();

        return new SuccessResult("Payment completed successfully.");
    }

    public Result GetOrderItems(int orderId)
    {
        var orderItems = _context.OrderItems
            .Where(x => x.OrderId == orderId && x.Order.UserId == _userSession.UserId)
            .ProjectToType<OrderItemView>().ToList();

        return new SuccessResult(orderItems);
    }

    public async Task<Result> ListOrders(PagingOptionModel request)
    {
        var allOrders = await _context.Orders
            .Where(x => x.UserId == _userSession.UserId)
            .ProjectToType<OrderView>()
            .ToPaginatedListAsync(request.PageIndex, request.PageSize);

        return new SuccessResult(allOrders);
    }

    public async Task<Result> PlaceOrder()
    {
        var carts = _context.Carts
            .Where(c => c.UserId == _userSession.UserId);

        if (!carts.Any()) return new ErrorResult("Cart is empty.");

        var cartItems = await carts
            .Select(c => new OrderItem
            {
                BookId = c.BookId,
                Quantity = c.Quantity,
                Amount = c.Book.Amount,
                IsStillAvailable = c.Book.TotalStock >= c.Quantity
            }).ToListAsync();

        if (cartItems.Any(c => !c.IsStillAvailable))
            return new ErrorResult("One or more books is no longer available in the quantity required. Kindly adjust your cart before you proceed.");

        var user = await _context.Users
            .Where(u => u.Id == _userSession.UserId)
            .Select(u => new
            {
                u.Email,
                u.FirstName,
                u.LastName
            }).FirstOrDefaultAsync();

        var newOrder = new Order
        {
            UserId = _userSession.UserId,
            TotalAmount = cartItems.Sum(c => c.Amount * c.Quantity),
            Items = cartItems
        };

        await _context.AddAsync(newOrder);

        var transactionModel = new InitiateTransactionModel
        {
            email = user.Email,
            // convert total amount to string and remove the decimal for paystack endpoint
            amount = newOrder.TotalAmount.ToString("F").Replace(".", ""),
            metadata = JsonSerializer.Serialize(user)
        };

        var initiatedTransaction = await InitiateTransaction(transactionModel);
        if (!initiatedTransaction.status) return new ErrorResult(initiatedTransaction.message);

        initiatedTransaction.data.Adapt(newOrder);

        _context.RemoveRange(carts);

        int saved = await _context.SaveChangesAsync();

        return saved > 0
            ? new SuccessResult("Order placed successfully.", newOrder.Adapt<PaymentRequestView>())
            : new ErrorResult("An error occurred while placing the order");
    }

    private async Task<PaystackResponse<TransactionResponse>> InitiateTransaction(InitiateTransactionModel model)
    {
        StringContent jsonContent = model.ToJsonContent();

        using HttpResponseMessage httpResponse = await _httpClient.PostAsync("/transaction/initialize", jsonContent);

        httpResponse.EnsureSuccessStatusCode();

        string responseString = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<PaystackResponse<TransactionResponse>>(responseString);

        return response;
    }

    private async Task<PaystackResponse<VerifyTransactionResponse>> VerifyTransaction(string reference)
    {
        using HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/transaction/verify/{reference}");

        httpResponse.EnsureSuccessStatusCode();

        string responseString = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<PaystackResponse<VerifyTransactionResponse>>(responseString);

        return response;
    }
}