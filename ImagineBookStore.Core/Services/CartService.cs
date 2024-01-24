using ImagineBookStore.Core.Interfaces;
using ImagineBookStore.Core.Models.App;
using ImagineBookStore.Core.Models.Input;
using ImagineBookStore.Core.Models.Utilities;
using ImagineBookStore.Core.Models.View;
using ImagineBookStore.Core.Utilities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ImagineBookStore.Core.Services;

/// <summary>
/// Implementation of shopping cart service operations.
/// </summary>
public class CartService : ICartService
{
    private readonly BookStoreContext _context;
    private readonly UserSession _userSession;

    /// <summary>
    /// Initializes a new instance of the <see cref="CartService"/> class.
    /// </summary>
    /// <param name="context">The database context for shopping cart operations. See <see cref="BookStoreContext"/>.</param>
    /// <param name="userSession">The user session information. See <see cref="UserSession"/>.</param>
    public CartService(BookStoreContext context, UserSession userSession)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));
    }

    /// <inheritdoc cref="ICartService.AddToCart"/>
    public async Task<Result> AddToCart(CartModel model)
    {
        Book book = await _context.Books
            .Where(b => b.Id == model.BookId && !b.IsDeleted)
            .FirstOrDefaultAsync();

        if (book == null) return new ErrorResult("Invalid book.");

        if (book.TotalStock < model.Quantity)
            return new ErrorResult($"This book only has only {book.TotalStock} left in stock");

        Cart cart = await _context.Carts
            .Where(c => c.UserId == _userSession.UserId && c.BookId == book.Id)
            .FirstOrDefaultAsync();

        cart ??= new Cart
            {
                UserId = _userSession.UserId,
                BookId = book.Id
            };

        cart.Quantity = model.Quantity;
        cart.UpdatedAt = DateTime.UtcNow;

        await _context.AddAsync(cart);

        int saved = await _context.SaveChangesAsync();

        return saved > 0
            ? new SuccessResult("Book added to cart successfully.")
            : new ErrorResult(StaticErrorMessages.UnableToSaveChanges);
    }

    /// <inheritdoc cref="ICartService.ListCarts"/>
    public async Task<Result> ListCarts()
    {
        var cartItems = await _context.Carts
            .Where(c => c.UserId == _userSession.UserId)
            .ProjectToType<CartItemsView>()
            .ToListAsync();

        var result = new CartView
        {
            TotalAmount = cartItems.Sum(c => c.TotalAmount),
            Items = cartItems
        };

        return new SuccessResult(result);
    }

    /// <inheritdoc cref="ICartService.RemoveFromCart"/>
    public async Task<Result> RemoveFromCart(int cartId)
    {
        var cart = await _context.Carts.FindAsync(cartId);

        if (cart == null) {
            return new SuccessResult();
            }

            _context.Remove(cart);
        int saved = await _context.SaveChangesAsync();

        return saved > 0
            ? new SuccessResult("Book removed from cart successfully.")
            : new ErrorResult("Unable to remove book from cart at the moment, please try again.");
    }
}
