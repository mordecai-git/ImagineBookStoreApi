using System.ComponentModel.DataAnnotations.Schema;

namespace ImagineBookStore.Core.Models.App;

/// <summary>
/// Represents an item within an order in the BookStore application.
/// </summary>
public class OrderItem : BaseAppModel
{
    /// <summary>
    /// Gets or sets the ID of the order to which the item belongs.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the book associated with the order item.
    /// </summary>
    public int BookId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the book in the order.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the amount associated with the order item.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the reference to the order to which the item belongs.
    /// </summary>
    public Order Order { get; set; }

    /// <summary>
    /// Gets or sets the reference to the book associated with the order item.
    /// </summary>
    public Book Book { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the book is still available (not used in the database).
    /// </summary>
    [NotMapped]
    public bool IsStillAvailable { get; set; }
}