using ImagineBookStore.Model.Input;
using ImagineBookStore.Model.Utilities;
using ImagineBookStore.Model.View;

namespace ImagineBookStore.Core.Interfaces;

/// <summary>
/// Interface for authentication services.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Creates a new user with the provided registration details.
    /// </summary>
    /// <param name="model">The registration model containing user details.</param>
    /// <returns>A task representing the asynchronous operation with the result of the user creation.
    /// The result type is <see cref="Result{T}"/> where T is <see cref="AuthDataView"/>.</returns>
    Task<Result> CreateUser(RegisterModel model);

    /// <summary>
    /// Authenticates a user with the provided login credentials.
    /// </summary>
    /// <param name="model">The login model containing user credentials.</param>
    /// <returns>A task with the authentication result: <see cref="Result{T}"/> where T is <see cref="AuthDataView"/>.</returns>
    Task<Result> AuthenticateUser(LoginModel model);

    /// <summary>
    /// Logs out the user with the specified reference.
    /// </summary>
    /// <param name="userReference">The reference of the user to log out.</param>
    /// <returns>A task with the logout result: <see cref="Result"/>.</returns>
    Task<Result> Logout(string userReference);
}