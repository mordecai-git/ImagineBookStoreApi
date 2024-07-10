using ImagineBookStore.Model.Input;
using ImagineBookStore.Model.Utilities;
using ImagineBookStore.Model.View;

namespace ImagineBookStore.Core.Interfaces;

/// <summary>
/// Interface for order-related service operations.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Attempts to process payment for the specified order.
    /// </summary>
    /// <param name="orderId">The ID of the order to attempt payment for.</param>
    /// <returns>A task with the result of the payment attempt: <see cref="Result"/> where T is <see cref="PaymentRequestView"/>.</returns>
    Task<Result> AttemptPayment(int orderId);

    /// <summary>
    /// Confirms payment for the specified order.
    /// </summary>
    /// <param name="orderId">The ID of the order to confirm payment for.</param>
    /// <returns>A task with the result of confirming the payment: <see cref="Result"/>.</returns>
    Task<Result> ConfirmPayment(int orderId);

    /// <summary>
    /// Gets the items of the specified order.
    /// </summary>
    /// <param name="orderId">The ID of the order to retrieve items for.</param>
    /// <returns>The result containing the order items: <see cref="Result{T}"/> where T is a list of <see cref="OrderItemView"/>.</returns>
    Result GetOrderItems(int orderId);

    /// <summary>
    /// Lists orders based on the provided paging options.
    /// </summary>
    /// <param name="request">The paging options for listing orders.</param>
    /// <returns>A task with the result of listing orders: <see cref="Result{T}"/> where T is a list of <see cref="OrderView"/>.</returns>
    Task<Result> ListOrders(PagingOptionModel request);

    /// <summary>
    /// Places a new order based on the items in the user's shopping cart.
    /// </summary>
    /// <returns>A task with the result of placing the order: <see cref="Result{T}"/> where T is <see cref="PaymentRequestView"/>.</returns>
    Task<Result> PlaceOrder();
}