using FluentValidation;

namespace ImagineBookStore.Model.Input;

/// <summary>
/// Represents a model for cart information, including quantity and book identifier.
/// </summary>
public class CartModel
{
    /// <summary>
    /// Quantity of the book in the cart.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Identifier for the associated book.
    /// </summary>
    public int BookId { get; set; }
}

/// <summary>
/// Validator for the <see cref="CartModel"/> class.
/// </summary>
public class CartModelValidator : AbstractValidator<CartModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CartModelValidator"/> class.
    /// Defines validation rules for the <see cref="CartModel"/>.
    /// </summary>
    public CartModelValidator()
    {
        RuleFor(c => c.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be at least one");

        RuleFor(c => c.BookId)
            .GreaterThan(0).WithMessage("Invalid book provided.");
    }
}