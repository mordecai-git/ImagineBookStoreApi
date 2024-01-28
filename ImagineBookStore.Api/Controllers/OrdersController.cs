using ImagineBookStore.Core.Interfaces;
using ImagineBookStore.Core.Models.Input;
using ImagineBookStore.Core.Models.Utilities;
using ImagineBookStore.Core.Models.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImagineBookStore.Api.Controllers;

/// <summary>
/// API controller for managing orders in the application.
/// </summary>
[ApiController]
[Route("api/v1/orders")]
[Authorize]
[Produces("application/json")]
public class OrdersController : BaseController
{
    private readonly IOrderService _orderService;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrdersController"/> class.
    /// </summary>
    /// <param name="orderService">The order service to be injected.</param>
    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
    }

    /// <summary>
    /// Attempts to retry payment for an order.
    /// </summary>
    /// <remarks>
    /// This endpoint attempts to retry payment for an order with the specified identifier. <br/>
    /// Requires authentication.
    /// </remarks>
    /// <param name="orderId">The identifier of the order for which payment is to be retried.</param>
    /// <response code="200">Returns the payment information.</response>
    /// <response code="404">Returns Not Found if the order for which payment should be retried does not exist.</response>
    [HttpGet("{orderId}/retry-payment")]
    [ProducesResponseType(typeof(SuccessResult<OrderView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundErrorResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AttemptPayment(int orderId)
    {
        var res = await _orderService.AttemptPayment(orderId);
        return ProcessResponse(res);
    }

    /// <summary>
    /// Confirms payment for an order.
    /// </summary>
    /// <remarks>
    /// This endpoint confirms payment for an order with the specified identifier. <br/>
    /// Requires authentication.
    /// </remarks>
    /// <param name="orderId">The identifier of the order for which payment is to be confirmed.</param>
    /// <response code="200">Returns the updated order information.</response>
    /// <response code="404">Returns Not Found if order for which payment should be confirmed does not exist.</response>
    /// <response code="400">Returns an error if any occurred.</response>
    [HttpGet("{orderId}/confirm-payment")]
    [ProducesResponseType(typeof(SuccessResult<OrderView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundErrorResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmPayment(int orderId)
    {
        var result = await _orderService.ConfirmPayment(orderId);
        return ProcessResponse(result);
    }

    /// <summary>
    /// Retrieves items in an order.
    /// </summary>
    /// <remarks>
    /// This endpoint retrieves items in an order with the specified identifier. <br/>
    /// Requires authentication.
    /// </remarks>
    /// <param name="orderId">The identifier of the order for which items are to be retrieved.</param>
    /// <response code="200">Returns the list of items in the order.</response>
    [HttpGet("{orderId}")]
    [ProducesResponseType(typeof(SuccessResult<List<OrderItemView>>), StatusCodes.Status200OK)]
    public IActionResult GetOrderItems(int orderId)
    {
        var result = _orderService.GetOrderItems(orderId);
        return ProcessResponse(result);
    }

    /// <summary>
    /// Lists orders.
    /// </summary>
    /// <remarks>
    /// This endpoint lists orders based on the provided paging options. <br/>
    /// Requires authentication.
    /// </remarks>
    /// <param name="request">The parameters containing the paging options.</param>
    /// <response code="200">Returns the list of orders in a paginated format.</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedSuccessResult<OrderView>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListOrders([FromQuery] PagingOptionModel request)
    {
        var result = await _orderService.ListOrders(request);
        return ProcessResponse(result);
    }

    /// <summary>
    /// Places a new order in the application.
    /// </summary>
    /// <remarks>
    /// This endpoint places a new order in the application. <br/>
    /// Requires authentication.
    /// </remarks>
    /// <response code="200">Returns the payment information required to continue the payment.</response>
    /// <response code="400">Returns an error object indicating what went wrong.</response>
    [HttpPost]
    [ProducesResponseType(typeof(SuccessResult<OrderView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PlaceOrder()
    {
        var result = await _orderService.PlaceOrder();
        return ProcessResponse(result);
    }
}