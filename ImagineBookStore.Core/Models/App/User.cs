using System.ComponentModel.DataAnnotations;

namespace ImagineBookStore.Core.Models.App;

public partial class User : BaseAppModel
{
    public Guid Uid { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public string Email { get; internal set; }

    [MaxLength(50)]
    public string FirstName { get; set; }

    [MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(255)]
    public string HashedPassword { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; }
}