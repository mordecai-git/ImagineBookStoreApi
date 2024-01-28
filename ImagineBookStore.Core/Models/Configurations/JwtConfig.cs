namespace ImagineBookStore.Core.Models.Configurations;

/// <summary>
/// Represents the configuration settings for JWT (JSON Web Token) authentication.
/// </summary>
public class JwtConfig
{
    /// <summary>
    /// Gets or sets the audience of the JWT token.
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// Gets or sets the expiration duration of the JWT token in minutes.
    /// </summary>
    public int Expires { get; set; }

    /// <summary>
    /// Gets or sets the issuer of the JWT token.
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// Gets or sets the expiration duration of the JWT refresh token in days.
    /// </summary>
    public int RefreshExpireDays { get; set; }

    /// <summary>
    /// Gets or sets the secret key used for signing the JWT token.
    /// </summary>
    public string Secret { get; set; }
}