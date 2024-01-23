using ImagineBookStore.Core.Models.Input;
using ImagineBookStore.Core.Models.Utilities;

namespace ImagineBookStore.Core.Interfaces;

public interface IOrderService
{
    Task<Result> AttemptPayment(int orderId);

    Task<Result> ConfirmPayment(int orderId);

    Result GetOrderItems(int orderId);

    Task<Result> ListOrders(PagingOptionModel request);

    Task<Result> PlaceOrder();
}
