using System.Text.Json.Serialization;

namespace ImagineBookStore.Core.Models.View;

/// <summary>
/// Represents a view model for displaying user information.
/// </summary>
public class UserView
{
    /// <summary>
    /// Identifier for the user (JsonIgnore).
    /// </summary>
    [JsonIgnore]
    public int Id { get; set; }

    /// <summary>
    /// Unique identifier (UID) associated with the user.
    /// </summary>
    public string Uid { get; set; }

    /// <summary>
    /// Email address of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// First name of the user.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Last name of the user.
    /// </summary>
    public string LastName { get; set; }
}
