using FluentValidation;

namespace ImagineBookStore.Core.Models.Input;

/// <summary>
/// Represents the model for a book.
/// </summary>
public class BookModel
{
    /// <summary>
    /// The title of the book.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The author of the book.
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// The genre of the book.
    /// </summary>
    public string Genre { get; set; }

    /// <summary>
    /// The total number of copies available.
    /// </summary>
    public int TotalStock { get; set; }
}

/// <summary>
/// Validator for the <see cref="BookModel"/> class.
/// </summary>
public class BookModelValidator : AbstractValidator<BookModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BookModelValidator"/> class.
    /// </summary>
    public BookModelValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Author).NotEmpty();
        RuleFor(x => x.Genre).NotEmpty();
        RuleFor(x => x.TotalStock).GreaterThan(0);
    }
}