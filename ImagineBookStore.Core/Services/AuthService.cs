using ImagineBookStore.Core.Interfaces;
using ImagineBookStore.Core.Models.App;
using ImagineBookStore.Core.Models.Input;
using ImagineBookStore.Core.Models.Utilities;
using ImagineBookStore.Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ImagineBookStore.Core.Services;

/// <summary>
/// Implementation of the authentication services.
/// </summary>
public class AuthService : IAuthService
{
    private readonly BookStoreContext _context;
    private readonly ITokenGenerator _tokenGenerator;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthService"/> class.
    /// </summary>
    /// <param name="context">The database context for user information. See <see cref="BookStoreContext"/>.</param>
    /// <param name="tokenGenerator">The token generator for creating and invalidating user tokens. See <see cref="ITokenGenerator"/>.</param>
    public AuthService(BookStoreContext context, ITokenGenerator tokenGenerator)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
    }

    /// <inheritdoc cref="IAuthService.AuthenticateUser"/>
    public async Task<Result> AuthenticateUser(LoginModel model)
    {
        model.Email = model.Email.ToLower().Trim();
        User user = await _context.Users
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.Email.ToLower() == model.Email);

        if (user == null)
            return new ErrorResult("Login Failed:", "User does not exist.");

        if (!user.HashedPassword.VerifyPassword(model.Password))
            return new ErrorResult("Login Failed:", "Invalid email or password");

        return await _tokenGenerator.GenerateJwtToken(user);
    }

    /// <inheritdoc cref="IAuthService.CreateUser"/>
    public async Task<Result> CreateUser(RegisterModel model)
    {
        // validate user with email doesn't exist
        var userExist = await _context.Users
            .AnyAsync(u => u.Email.ToLower().Trim() == model.Email.ToLower().Trim());

        if (userExist)
            return new ErrorResult("An account with this email already exist. Please log in instead.");

        // create user object
        var user = new User
        {
            Email = model.Email.Trim(),
            FirstName = model.FirstName,
            LastName = model.LastName,
            HashedPassword = model.Password.HashPassword()
        };

        // save user
        await _context.AddAsync(user);
        int saved = await _context.SaveChangesAsync();
        if (saved < 1)
            return new ErrorResult("Unable to add user at the moment. Please try again");

        // create user token
        var authData = await _tokenGenerator.GenerateJwtToken(user);

        // return user token
        if (!authData.Success)
            return new ErrorResult(authData.Message);

        return new SuccessResult(StatusCodes.Status201Created, authData.Content);
    }

    /// <inheritdoc cref="IAuthService.Logout"/>
    public async Task<Result> Logout(string userReference)
    {
        await _tokenGenerator.InvalidateToken(userReference);
        return new SuccessResult();
    }

    public async Taks<Result> AddUserToRole(Guid uid, string roleName)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        if (role == null) return new BadErrorResult("Invalid role provided.");

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Uid == uid);
        if (user == null) return new BadErrorResult("Invalid user provided.");

        var userRole = await _context.UserRoles
            .Where(ur => ur.RoleId == role.Id && ur.UserId == user.Id)
            .FirstOrDefaultAsync();

        if (userRole != null) return new SuccessResult();

        userRole = new()
        {
            RoleId = role.Id,
            UserId = user.Id
        };

        await _context.AddAsync(userRole);

        int saved = await _context.SaveChangesAsync();

        return saved > 0
            ? new SuccessResult()
            : new ErrorResult("Unabe to add user to role, please try again later.");
    }
}