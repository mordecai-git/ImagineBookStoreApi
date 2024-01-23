namespace ImagineBookStore.Core.Models.App;

public class Order : BaseAppModel
{
    public int UserId { get; set; }

    public decimal TotalAmount { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public bool IsPaid { get; set; } = false;
    public string Authorization_Url { get; set; }
    public string Access_Code { get; set; }
    public string Reference { get; set; }

    public User User { get; set; }
    public ICollection<OrderItem> Items { get; set;}
}