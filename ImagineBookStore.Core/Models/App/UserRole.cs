using System.ComponentModel.DataAnnotations;

namespace ImagineBookStore.Core.Models.App;

/// <summary>
/// Represents the association between users and roles in the BookStore application.
/// </summary>
public partial class UserRole : BaseAppModel
{
    /// <summary>
    /// Gets or sets the foreign key to the user this role is attached to.
    /// </summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the foreign key to the role this user possesses.
    /// </summary>
    [Required]
    public int RoleId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the role associated with this user role.
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    /// Gets or sets the reference to the user associated with this user role.
    /// </summary>
    public User User { get; set; }
}