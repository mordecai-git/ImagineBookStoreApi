using ImagineBookStore.Core.Interfaces;
using ImagineBookStore.Core.Models.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImagineBookStore.Api.Controllers;

[ApiController]
[Route("api/v1/orders")]
[Authorize]
public class OrdersController : BaseController
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
    }

    [HttpGet("{orderId}/retry-payment")]
    public async Task<IActionResult> AttemptPayment(int orderId)
    {
        var res = await _orderService.AttemptPayment(orderId);
        return ProcessResponse(res);
    }

    [HttpGet("{orderId}/confirm-payment")]
    public async Task<IActionResult> ConfirmPayment(int orderId)
    {
        var result = await _orderService.ConfirmPayment(orderId);
        return ProcessResponse(result);
    }

    [HttpGet("{orderId}")]
    public IActionResult GetOrderItems(int orderId)
    {
        var result = _orderService.GetOrderItems(orderId);
        return ProcessResponse(result);
    }

    [HttpGet]
    public async Task<IActionResult> ListOrders([FromQuery] PagingOptionModel request)
    {
        var result = await _orderService.ListOrders(request);
        return ProcessResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder()
    {
        var result = await _orderService.PlaceOrder();
        return ProcessResponse(result);
    }
}