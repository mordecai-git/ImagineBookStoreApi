namespace ImagineBookStore.Core.Models.Input;

/// <summary>
/// Represents the user session information.
/// </summary>
public class UserSession
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public string Uid { get; set; }

    /// <summary>
    /// Gets or sets the business ID.
    /// </summary>
    public int? BusinessId { get; set; }
}