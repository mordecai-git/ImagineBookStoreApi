namespace ImagineBookStore.Core.Models.View;

public class OrderItemView
{
    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public string BookAuthor { get; set; }

    public int Quantity { get; set; }
    public decimal Amount { get; set; }
    public decimal TotalAmount
    {
        get
        {
            return Quantity * Amount;
        }
    }
}
