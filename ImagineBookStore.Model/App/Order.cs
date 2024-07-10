namespace ImagineBookStore.Model.App;

/// <summary>
/// Represents an order in the BookStore application.
/// </summary>
public class Order : BaseAppModel
{
    /// <summary>
    /// Gets or sets the ID of the user who placed the order.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the total amount associated with the order.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the order was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets a value indicating whether the order has been paid.
    /// </summary>
    public bool IsPaid { get; set; } = false;

    /// <summary>
    /// Gets or sets the authorization URL associated with the payment.
    /// </summary>
    public string Authorization_Url { get; set; }

    /// <summary>
    /// Gets or sets the access code associated with the payment.
    /// </summary>
    public string Access_Code { get; set; }

    /// <summary>
    /// Gets or sets the reference associated with the order.
    /// </summary>
    public string Reference { get; set; }

    /// <summary>
    /// Gets or sets the reference to the user who placed the order.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Gets or sets the collection of items associated with the order.
    /// </summary>
    public ICollection<OrderItem> Items { get; set; }
}