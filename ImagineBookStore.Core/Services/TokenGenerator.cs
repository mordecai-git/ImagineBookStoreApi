using ImagineBookStore.Core.Constants;
using ImagineBookStore.Core.Interfaces;
using ImagineBookStore.Model.App;
using ImagineBookStore.Model.Configurations;
using ImagineBookStore.Model.Utilities;
using ImagineBookStore.Model.View;
using Mapster;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ImagineBookStore.Core.Services;

/// <summary>
/// Class responsible for generating and invalidating JWT tokens for user authentication. <see cref="User"/>
/// </summary>
public class TokenGenerator : ITokenGenerator
{
    private readonly JwtConfig _jwtConfig;
    private readonly ICacheService _cacheService;

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenGenerator"/> class.
    /// </summary>
    /// <param name="jwtConfig">The JWT configuration options. See <see cref="IOptions{JwtConfig}"/>.</param>
    /// <param name="cacheService">The cache service for token storage. See <see cref="ICacheService"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown when either jwtConfig or cacheService is null.</exception>
    public TokenGenerator(IOptions<JwtConfig> jwtConfig, ICacheService cacheService)
    {
        _jwtConfig = jwtConfig.Value ?? throw new ArgumentNullException(nameof(jwtConfig));
        _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Task<Result> GenerateJwtToken(User user)
    {
        // Set expiration time for the token
        DateTime expiresAt = DateTime.UtcNow.AddDays(_jwtConfig.Expires);

        // Generate the access token
        var token = GenerateAccessToken(user, expiresAt);

        // Cache the token
        _cacheService.AddToken($"{AuthKeys.TokenCacheKey}{user.Uid}", token, expiresAt);

        // Prepare the result with user information, token, and expiration time
        var result = new AuthDataView
        {
            User = user.Adapt<UserView>(),
            Token = token,
            ExpiresAt = expiresAt
        };

        return Task.FromResult<Result>(new SuccessResult(result));
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Task InvalidateToken(string userReference)
    {
        // Remove the token from the cache
        _cacheService.RemoveToken($"{AuthKeys.TokenCacheKey}{userReference}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Generates an access token for the specified user with the given expiration time.
    /// </summary>
    /// <param name="user">The user for whom the token is generated. See <see cref="User"/>.</param>
    /// <param name="expiresAt">The expiration time for the token. See <see cref="DateTime"/>.</param>
    /// <returns>The generated access token as a string.</returns>
    private string GenerateAccessToken(User user, DateTime expiresAt)
    {
        // Create a token handler
        var tokenHandler = new JwtSecurityTokenHandler();

        // Create a claims identity for the user
        var claimIdentity = new ClaimsIdentity();

        // Add user-specific claims to the identity
        claimIdentity.AddClaims(new[] { new Claim("uid", user.Uid.ToString()) });
        claimIdentity.AddClaims(new[] { new Claim("sid", user.Id.ToString()) });

        // Add user roles as claims if available
        if (user.UserRoles != null && user.UserRoles.Any())
            claimIdentity.AddClaims(user.UserRoles.Select(role =>
                new Claim(ClaimTypes.Role, role.Role.Name)));

        // Get the secret key as bytes
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        // Create a token descriptor
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _jwtConfig.Audience,
            Issuer = _jwtConfig.Issuer,
            Subject = claimIdentity,
            Expires = expiresAt,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        // Create a security token based on the descriptor
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        // Write the token as a string
        var token = tokenHandler.WriteToken(securityToken);

        return token;
    }
}