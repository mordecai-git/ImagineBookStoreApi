﻿using ImagineBookStore.Core.Constants;
using ImagineBookStore.Core.Interfaces;
using ImagineBookStore.Core.Models.Configurations;
using ImagineBookStore.Core.Models.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace ImagineBookStore.Core.Middlewares;

public class JWTMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly ICacheService _cacheService;

    public JWTMiddleware(RequestDelegate next, IConfiguration configuration, ICacheService cacheService)
    {
        _next = next;
        _configuration = configuration;
        _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
    }

    public async Task Invoke(HttpContext context, IOptions<JwtConfig> jwtConfig)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null && !IsAnonymous(context))
        {
            // attach the token to the request
            if (await AttachAccountToContext(context, token, jwtConfig.Value))
            {
                await _next(context);
            }
        }
        else
        {
            await _next(context);
        }
    }

    private static bool IsAnonymous(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint is RouteEndpoint routeEndpoint)
        {
            // Check if the action method is decorated with AllowAnonymous attribute
            var actionDescriptor = routeEndpoint.Metadata.GetMetadata<ControllerActionDescriptor>();

            var controllerAllowAnonymousAttribute =
                actionDescriptor?.ControllerTypeInfo.GetCustomAttributes(inherit: true)
                .OfType<AllowAnonymousAttribute>().Any();

            var methodAllowAnonymousAttribute =
                actionDescriptor?.MethodInfo.GetCustomAttributes(inherit: true)
                .OfType<AllowAnonymousAttribute>().Any();

            if (controllerAllowAnonymousAttribute.HasValue && controllerAllowAnonymousAttribute.Value
                || methodAllowAnonymousAttribute.HasValue && methodAllowAnonymousAttribute.Value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    private async Task<bool> AttachAccountToContext(HttpContext context, string token, JwtConfig jwtConfig)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtConfig.Audience,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var id = jwtToken.Claims.First(x => x.Type == "sid").Value;
            var uid = jwtToken.Claims.First(x => x.Type == "uid").Value;

            //check if token is string in the cache
            string sToken = await _cacheService.GetToken($"{AuthKeys.TokenCacheKey}{uid}");
            if (string.IsNullOrEmpty(sToken) || sToken != token)
            {
                context.Items["User"] = null;
                context.User = null;
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return false;
            };

            // attach account to context on successful jwt validation
            context.Items["User"] = new UserView()
            {
                Uid = uid,
                Id = int.Parse(id)
            };

            return true;
        }
        catch
        {
            // do nothing if jwt validation fails
            // account is not attached to context so request won't have access to secure routes
        }

        return false;
    }
}