using System.ComponentModel.DataAnnotations;

namespace ImagineBookStore.Core.Models.App;

public class Cart : BaseAppModel
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int BookId { get; set; }

    [Required]
    public int Quantity { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; }

    public Book Book { get; set; }
}
