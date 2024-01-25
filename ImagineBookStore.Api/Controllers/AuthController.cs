using ImagineBookStore.Core.Interfaces;
using ImagineBookStore.Core.Models.Input;
using ImagineBookStore.Core.Models.Utilities;
using ImagineBookStore.Core.Models.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImagineBookStore.Api.Controllers;

/// <summary>
/// API controller for managing authentication in the application.
/// </summary>
[ApiController]
[Route("api/v1/auth")]
[Authorize]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="authService">The authentication service to be injected.</param>
    public AuthController(IAuthService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    /// <summary>
    /// Authenticates a user and generates an authentication token.
    /// </summary>
    /// <remarks>
    /// This endpoint authenticates a user and generates an authentication token. <br/>
    /// Requires no authentication.
    /// </remarks>
    /// <param name="model">The model representing the user's login credentials.</param>
    /// <response code="200">Returns the authenticated user's authentication data.</response>
    /// <response code="400">Returns an error object indicating what went wrong.</response>
    [HttpPost("token")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult<AuthDataView>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> AuthenticateUser(LoginModel model)
    {
        var res = await _authService.AuthenticateUser(model);
        return ProcessResponse(res);
    }

    /// <summary>
    /// Logs out a user with the specified user reference.
    /// </summary>
    /// <remarks>
    /// This endpoint logs out a user with the specified user reference. <br/>
    /// Requires no authentication.
    /// </remarks>
    /// <param name="userReference">The user reference for whom the logout operation should be performed.</param>
    /// <response code="200">Returns a successful message if the logout operation is successful.</response>
    [HttpPost("{userReference}/logout")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
    public async Task<IActionResult> LogoutUser([FromRoute] string userReference)
    {
        var res = await _authService.Logout(userReference);
        return ProcessResponse(res);
    }

    /// <summary>
    /// Signs up a new user in the application.
    /// </summary>
    /// <remarks>
    /// This endpoint creates a new user account. <br/>
    /// Requires no authentication.
    /// </remarks>
    /// <param name="model">The model representing the user registration data.</param>
    /// <response code="201">Returns the created user's authentication data.</response>
    /// <response code="400">Returns an error object indicating what went wrong.</response>
    [HttpPost("sign-up")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SuccessResult<AuthDataView>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> SignUp(RegisterModel model)
    {
        var res = await _authService.CreateUser(model);
        return ProcessResponse(res);
    }
}