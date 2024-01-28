using ImagineBookStore.Core.Models.App;
using ImagineBookStore.Core.Models.Utilities;

namespace ImagineBookStore.Core.Interfaces;

/// <summary>
/// Defines the contract for generating and invalidating JWT tokens.
/// </summary>
public interface ITokenGenerator
{
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the token is generated.</param>
    /// <returns>A task representing the asynchronous operation with the result containing the generated token. </returns>
    Task<Result> GenerateJwtToken(User user);

    /// <summary>
    /// Invalidates the JWT token for the specified user reference.
    /// </summary>
    /// <param name="userReference">The user reference for whom the token should be invalidated. See <see cref="string"/>.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task InvalidateToken(string userReference);
}