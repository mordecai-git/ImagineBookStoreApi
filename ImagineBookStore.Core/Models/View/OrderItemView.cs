namespace ImagineBookStore.Core.Models.View;

/// <summary>
/// Represents a view model for displaying information about an item in an order.
/// </summary>
public class OrderItemView
{
    /// <summary>
    /// Identifier for the associated book in the order item.
    /// </summary>
    public int BookId { get; set; }

    /// <summary>
    /// Title of the associated book in the order item.
    /// </summary>
    public string BookTitle { get; set; }

    /// <summary>
    /// Author of the associated book in the order item.
    /// </summary>
    public string BookAuthor { get; set; }

    /// <summary>
    /// Quantity of the associated book in the order item.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Amount of the associated book in the order item.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Total amount for the specific order item (quantity * amount).
    /// </summary>
    public decimal TotalAmount
    {
        get
        {
            return Quantity * Amount;
        }
    }
}