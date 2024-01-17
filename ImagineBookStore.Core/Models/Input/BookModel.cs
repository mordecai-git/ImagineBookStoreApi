using FluentValidation;

namespace ImagineBookStore.Core.Models.Input
{
    public class BookModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int TotalStock { get; set; }
    }

    public class BookModelValidator : AbstractValidator<BookModel>
    {
        public BookModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Author).NotEmpty();
            RuleFor(x => x.Genre).NotEmpty();
            RuleFor(x => x.TotalStock).GreaterThan(0);
        }
    }
}
