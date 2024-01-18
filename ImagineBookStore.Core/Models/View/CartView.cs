namespace ImagineBookStore.Core.Models.View;

public class CartView
{
    public decimal TotalPrice { get; set; }
    public List<CartItemsView> Items { get; set; }
}

public class CartItemsView
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public string BookAuthor { get; set; }
    public decimal BookPrice { get; set; }

    public int Quantity { get; set; }
    public decimal TotalPrice
    {
        get
        {
            return Quantity * BookPrice;
        }
    }

    public DateTime AddedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
