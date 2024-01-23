namespace ImagineBookStore.Core.Models.View;

public class OrderView
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsPaid { get; set; }
    public DateTime UpdatedAt { get; set; }
}
