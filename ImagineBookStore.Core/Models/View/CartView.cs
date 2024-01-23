namespace ImagineBookStore.Core.Models.View;

public class CartView
{
    public decimal TotalAmount { get; set; }
    public List<CartItemsView> Items { get; set; }
}

public class CartItemsView
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public string BookAuthor { get; set; }
    public decimal BookAmount { get; set; }

    public int Quantity { get; set; }
    public decimal TotalAmount
    {
        get
        {
            return Quantity * BookAmount;
        }
    }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsStillAvailable { get; set; }
}
