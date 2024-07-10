using System.ComponentModel.DataAnnotations;

namespace ImagineBookStore.Model.App;

/// <summary>
/// Represents a user in the BookStore application.
/// </summary>
public partial class User : BaseAppModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public Guid Uid { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    [MaxLength(50)]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    [MaxLength(50)]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the hashed password of the user.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string HashedPassword { get; set; }

    /// <summary>
    /// Gets or sets the collection of roles associated with this user.
    /// </summary>
    public virtual ICollection<UserRole> UserRoles { get; set; }
}