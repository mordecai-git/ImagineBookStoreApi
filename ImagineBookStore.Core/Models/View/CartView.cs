namespace ImagineBookStore.Core.Models.View;

/// <summary>
/// Represents a view model for displaying cart information.
/// </summary>
public class CartView
{
    /// <summary>
    /// Total amount of the items in the cart.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// List of cart items in the cart view.
    /// </summary>
    public List<CartItemsView> Items { get; set; }
}

/// <summary>
/// Represents a view model for displaying individual cart items.
/// </summary>
public class CartItemsView
{
    /// <summary>
    /// Identifier for the cart item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identifier for the associated book.
    /// </summary>
    public int BookId { get; set; }

    /// <summary>
    /// Title of the associated book.
    /// </summary>
    public string BookTitle { get; set; }

    /// <summary>
    /// Author of the associated book.
    /// </summary>
    public string BookAuthor { get; set; }

    /// <summary>
    /// Amount of the associated book.
    /// </summary>
    public decimal BookAmount { get; set; }

    /// <summary>
    /// Quantity of the book in the cart.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Total amount for the specific cart item (quantity * book amount).
    /// </summary>
    public decimal TotalAmount
    {
        get
        {
            return Quantity * BookAmount;
        }
    }

    /// <summary>
    /// Date and time when the cart item was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date and time when the cart item was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Indicates whether the associated book is still available.
    /// </summary>
    public bool IsStillAvailable { get; set; }
}