namespace ImagineBookStore.Model.View;

/// <summary>
/// Represents a view model for displaying information about an order.
/// </summary>
public class OrderView
{
    /// <summary>
    /// Identifier for the order.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Date and time when the order was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Total amount for the items in the order.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Indicates whether the order has been paid.
    /// </summary>
    public bool IsPaid { get; set; }

    /// <summary>
    /// Date and time when the order was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}