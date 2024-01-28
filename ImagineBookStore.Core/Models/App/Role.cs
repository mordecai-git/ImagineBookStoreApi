using System.ComponentModel.DataAnnotations;

namespace ImagineBookStore.Core.Models.App;

/// <summary>
/// Represents a role in the BookStore application.
/// </summary>
public class Role
{
    /// <summary>
    /// Gets or sets the unique identifier for the role.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the role.
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the collection of user roles associated with this role.
    /// </summary>
    public ICollection<UserRole> UserRoles { get; set; }
}