namespace ImagineBookStore.Model.View;

/// <summary>
/// Represents the view model for authentication data, including user information, authentication token, and expiration time.
/// </summary>
public class AuthDataView
{
    /// <summary>
    /// Gets or sets the user information.
    /// </summary>
    public UserView User { get; set; }

    /// <summary>
    /// Gets or sets the authentication token.
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Gets or sets the expiration time for the authentication token.
    /// </summary>
    public DateTime ExpiresAt { get; set; }
}