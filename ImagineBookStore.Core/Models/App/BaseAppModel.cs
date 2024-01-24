using System.ComponentModel.DataAnnotations;

namespace ImagineBookStore.Core.Models.App;

/// <summary>
/// Represents a base application model with common properties.
/// </summary>
public class BaseAppModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the model.
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp of the model in Coordinated Universal Time (UTC).
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}