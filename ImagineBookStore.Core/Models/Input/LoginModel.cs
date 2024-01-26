using FluentValidation;

namespace ImagineBookStore.Core.Models.Input;

/// <summary>
/// Represents the model for user login.
/// </summary>
public class LoginModel
{
    /// <summary>
    /// The email address of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// The password for authentication.
    /// </summary>
    public string Password { get; set; }
}

/// <summary>
/// Validator for the <see cref="LoginModel"/> class.
/// </summary>
public class LoginModelValidation : AbstractValidator<LoginModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginModelValidation"/> class.
    /// </summary>
    public LoginModelValidation()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}