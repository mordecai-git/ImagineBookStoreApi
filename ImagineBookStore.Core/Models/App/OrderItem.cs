using System.ComponentModel.DataAnnotations.Schema;

namespace ImagineBookStore.Core.Models.App;

public class OrderItem : BaseAppModel
{
    public int OrderId { get; set; }

    public int BookId { get; set; }

    public int Quantity { get; set; }

    public decimal Amount { get; set; }

    public Order Order { get; set; }
    public Book Book { get; set; }

    // use this for internal functions, not used in the database
    [NotMapped]
    public bool IsStillAvailable { get; set; }
}
