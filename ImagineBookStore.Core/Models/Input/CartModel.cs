using FluentValidation;

namespace ImagineBookStore.Core.Models.Input
{
    public class CartModel
    {
        public  int Quantity { get; set; }

        public int BookId { get; set; } 
    }

    public class CartModelValidator : AbstractValidator<CartModel>
    {
        public CartModelValidator()
        {
            RuleFor(c => c.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be at least one");

            RuleFor(c => c.BookId)
                .GreaterThan(0).WithMessage("Invalid book provided.");
        }
    }
}
