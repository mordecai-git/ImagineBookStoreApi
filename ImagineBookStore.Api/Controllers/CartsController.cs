using ImagineBookStore.Core.Interfaces;
using ImagineBookStore.Core.Models.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImagineBookStore.Api.Controllers;

[ApiController]
[Route("api/v1/carts")]
[AllowAnonymous] // TODO: remove this
// [Authorize] TODO: activate this
public class CartsController : BaseController
{
    private readonly ICartService _cartService;

    public CartsController(ICartService cartService)
    {
        _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(CartModel model)
    {
        var res = await _cartService.AddToCart(model);
        return ProcessResponse(res);
    }

    [HttpDelete("{cartId}")]
    public async Task<IActionResult> RemoveFromCart(int cartId)
    {
        var res = await _cartService.RemoveFromCart(cartId);
        return ProcessResponse(res);
    }

    [HttpGet]
    public async Task<IActionResult> ListCarts()
    {
        var res = await _cartService.ListCarts();
        return ProcessResponse(res);
    }
}