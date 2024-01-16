using System.ComponentModel.DataAnnotations;

namespace ImagineBookStore.Core.Models.App;

public partial class UserRole : BaseAppModel
{
    /// <summary>
    /// A foreign key to the user this role is attached to
    /// </summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// A foreign key to the role this user possess
    /// </summary>
    [Required]
    public int RoleId { get; set; }

    public Role Role { get; set; }
    public User User { get; set; }
}