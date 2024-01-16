using System.ComponentModel.DataAnnotations;

namespace ImagineBookStore.Core.Models.App;

public class BaseAppModel
{
    [Required]
    public int Id { get; set; }

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}
