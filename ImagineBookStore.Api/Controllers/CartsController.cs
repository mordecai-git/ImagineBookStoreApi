using ImagineBookStore.Core.Interfaces;
using ImagineBookStore.Model.Input;
using ImagineBookStore.Model.Utilities;
using ImagineBookStore.Model.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImagineBookStore.Api.Controllers;

/// <summary>
/// API controller for managing carts in the application.
/// </summary>
[ApiController]
[Route("api/v1/carts")]
[Authorize]
public class CartsController : BaseController
{
    private readonly ICartService _cartService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CartsController"/> class.
    /// </summary>
    /// <param name="cartService">The cart service to be injected.</param>
    public CartsController(ICartService cartService)
    {
        _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
    }

    /// <summary>
    /// Adds an item to the user's cart.
    /// </summary>
    /// <remarks>
    /// This endpoint adds an item to the user's cart. If the item already exist in the cart, it just updates the quantity of them item in the cart. <br/>
    /// Requires authentication.
    /// </remarks>
    /// <param name="model">The model representing the item to be added to the cart.</param>
    /// <response code="200">Returns a success response.</response>
    [HttpPost]
    [ProducesResponseType(typeof(SuccessResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddToCart(CartModel model)
    {
        var res = await _cartService.AddToCart(model);
        return ProcessResponse(res);
    }

    /// <summary>
    /// Removes an item from the user's cart.
    /// </summary>
    /// <remarks>
    /// This endpoint removes an item from the user's cart. <br/>
    /// Requires authentication.
    /// </remarks>
    /// <param name="cartId">The identifier of the item to be removed from the cart.</param>
    /// <response code="200">Returns the updated cart information.</response>
    /// <response code="404">Returns Not Found if item does not exist in the cart.</response>
    [HttpDelete("{cartId}")]
    [ProducesResponseType(typeof(SuccessResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundErrorResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveFromCart(int cartId)
    {
        var res = await _cartService.RemoveFromCart(cartId);
        return ProcessResponse(res);
    }

    /// <summary>
    /// Lists items in the user's cart.
    /// </summary>
    /// <remarks>
    /// This endpoint lists items in the user's cart. <br/>
    /// Requires authentication.
    /// </remarks>
    /// <response code="200">Returns the list of items in the user's cart.</response>
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResult<List<CartItemsView>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListCarts()
    {
        var res = await _cartService.ListCarts();
        return ProcessResponse(res);
    }
}