using ImagineBookStore.Model.Input;
using ImagineBookStore.Model.Utilities;
using ImagineBookStore.Model.View;

namespace ImagineBookStore.Core.Interfaces;

/// <summary>
/// Interface for shopping cart service operations.
/// </summary>
public interface ICartService
{
    /// <summary>
    /// Adds a book to the user's shopping cart.
    /// </summary>
    /// <param name="model">The cart model containing information about the book and quantity to add.</param>
    /// <returns>A task with the result of adding the book to the cart: <see cref="Result"/>.</returns>
    Task<Result> AddToCart(CartModel model);

    /// <summary>
    /// Lists the items in the user's shopping cart.
    /// </summary>
    /// <returns>A task with the result of listing the items in the cart: <see cref="Result{T}"/> where T is <see cref="CartView"/>.</returns>
    Task<Result> ListCarts();

    /// <summary>
    /// Removes a book from the user's shopping cart.
    /// </summary>
    /// <param name="cartId">The ID of the cart item to remove.</param>
    /// <returns>A task with the result of removing the book from the cart: <see cref="Result"/>.</returns>
    Task<Result> RemoveFromCart(int cartId);
}