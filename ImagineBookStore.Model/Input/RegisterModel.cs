using FluentValidation;

namespace ImagineBookStore.Model.Input;

/// <summary>
/// Represents the information required to register a new user.
/// </summary>
public class RegisterModel
{
    /// <summary>
    /// The email address of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// The first name of the user.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// The last name of the user.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// The password for the user.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// The confirmation password for the user.
    /// </summary>
    public string ConfirmPassword { get; set; }
}

/// <summary>
/// Represents the validation rules for the <see cref="RegisterModel"/>.
/// </summary>
public class RegisterModelValidation : AbstractValidator<RegisterModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterModelValidation"/> class.
    /// Defines validation rules for the <see cref="RegisterModel"/>.
    /// </summary>
    public RegisterModelValidation()
    {
        RuleFor(x => x.Email).EmailAddress();

        RuleFor(x => x.FirstName).NotNull();

        RuleFor(x => x.LastName).NotNull();

        RuleFor(x => x.ConfirmPassword).Equal(p => p.Password);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Your password cannot be empty")
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .MaximumLength(20).WithMessage("Your password length must not exceed 20.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
    }
}