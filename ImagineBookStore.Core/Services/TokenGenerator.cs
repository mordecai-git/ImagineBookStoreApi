using ImagineBookStore.Core.Constants;
using ImagineBookStore.Core.Interfaces;
using ImagineBookStore.Core.Models.App;
using ImagineBookStore.Core.Models.Configurations;
using ImagineBookStore.Core.Models.Utilities;
using ImagineBookStore.Core.Models.View;
using Mapster;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ImagineBookStore.Core.Services;

public class TokenGenerator : ITokenGenerator
{
    private readonly JwtConfig _jwtConfig;
    private readonly ICacheService _cacheService;

    public TokenGenerator(IOptions<JwtConfig> jwtConfig, ICacheService cacheService)
    {
        _jwtConfig = jwtConfig.Value ?? throw new ArgumentNullException(nameof(jwtConfig));
        _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
    }

    public async Task<Result> GenerateJwtToken(User user)
    {
        DateTime expiresAt = DateTime.UtcNow.AddDays(_jwtConfig.Expires);

        var token = GenerateAccessToken(user, expiresAt);

        //cache the token
        _cacheService.AddToken($"{AuthKeys.TokenCacheKey}{user.Uid}", token, expiresAt);

        var result = new AuthDataView
        {
            User = user.Adapt<UserView>(),
            Token = token,
            ExpiresAt = expiresAt
        };

        return new SuccessResult(result);
    }

    public async Task InvalidateToken(string userReference)
    {
        _cacheService.RemoveToken($"{AuthKeys.TokenCacheKey}{userReference}");
    }

    private string GenerateAccessToken(User user, DateTime expiresAt)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var claimIdentity = new ClaimsIdentity();

        claimIdentity.AddClaims(new[] { new Claim("uid", user.Uid.ToString()) });
        claimIdentity.AddClaims(new[] { new Claim("sid", user.Id.ToString()) });

        if (user.UserRoles != null && user.UserRoles.Any())
            claimIdentity.AddClaims(user.UserRoles.Select(role =>
                new Claim(ClaimTypes.Role, role.Role.Name)));

        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _jwtConfig.Audience,
            Issuer = _jwtConfig.Issuer,
            Subject = claimIdentity,
            Expires = expiresAt,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(securityToken);

        return token;
    }
}