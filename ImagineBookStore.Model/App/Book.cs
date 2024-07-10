using System.ComponentModel.DataAnnotations;

namespace ImagineBookStore.Model.App;

/// <summary>
/// Represents a book in the BookStore application.
/// </summary>
public class Book : BaseAppModel
{
    /// <summary>
    /// Gets or sets the title of the book.
    /// </summary>
    [Required]
    [StringLength(255)]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the author of the book.
    /// </summary>
    [Required]
    [StringLength(255)]
    public string Author { get; set; }

    /// <summary>
    /// Gets or sets the genre of the book.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Genre { get; set; }

    /// <summary>
    /// Gets or sets the price of the book.
    /// </summary>
    [Required]
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the total stock quantity of the book.
    /// </summary>
    [Required]
    public int TotalStock { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the book is deleted.
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// Gets or sets the ID of the user who deleted the book.
    /// </summary>
    public int? DeletedById { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the book was deleted.
    /// </summary>
    public DateTime DeletedAt { get; set; }

    /// <summary>
    /// Gets or sets the user who deleted the book.
    /// </summary>
    public User DeletedBy { get; set; }
}