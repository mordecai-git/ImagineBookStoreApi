using System.ComponentModel.DataAnnotations;

namespace ImagineBookStore.Model.App;

/// <summary>
/// Represents a user's shopping cart in the BookStore application.
/// </summary>
public class Cart : BaseAppModel
{
    /// <summary>
    /// Gets or sets the ID of the user associated with the cart.
    /// </summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the book added to the cart.
    /// </summary>
    [Required]
    public int BookId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the book added to the cart.
    /// </summary>
    [Required]
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the cart was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the user associated with the cart.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Gets or sets the book associated with the cart.
    /// </summary>
    public Book Book { get; set; }
}