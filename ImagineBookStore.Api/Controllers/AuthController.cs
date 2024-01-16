using ImagineBookStore.Core.Interfaces;
using ImagineBookStore.Core.Models.Input;
using ImagineBookStore.Core.Models.Utilities;
using ImagineBookStore.Core.Models.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImagineBookStore.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
[Authorize]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SuccessResult<AuthDataView>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> SignUp(RegisterModel model)
    {
        var res = await _authService.CreateUser(model);
        return ProcessResponse(res);
    }

    [HttpPost("token")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult<AuthDataView>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> AuthenticateUser(LoginModel model)
    {
        var res = await _authService.AuthenticateUser(model);
        return ProcessResponse(res);
    }

    [HttpPost("{userReference}/logout")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
    public async Task<IActionResult> AuthenticateUserAsync([FromRoute] string userReference)
    {
        var res = await _authService.Logout(userReference);
        return ProcessResponse(res);
    }
}