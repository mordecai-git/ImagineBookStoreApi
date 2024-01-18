using FluentValidation;
using ImagineBookStore.Core.Interfaces;
using ImagineBookStore.Core.Models.App;
using ImagineBookStore.Core.Models.Input;
using ImagineBookStore.Core.Models.View;
using ImagineBookStore.Core.Services;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;
using System.Text;

namespace ImagineBookStore.Core.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration, bool isProduction)
    {

        services.AddDbContext<BookStoreContext>(options =>
        {
            options.UseInMemoryDatabase("ImagineBookStoreDb");
        });

        // Add fluent validation.
        services.AddValidatorsFromAssembly(Assembly.Load("ImagineBookStore.Core"));
        services.AddFluentValidationAutoValidation(configuration =>
        {
            // Disable the built-in .NET model (data annotations) validation.
            configuration.DisableBuiltInModelValidation = true;

            // Enable validation for parameters bound from `BindingSource.Form` binding sources.
            configuration.EnableFormBindingSourceAutomaticValidation = true;

            // Enable validation for parameters bound from `BindingSource.Path` binding sources.
            configuration.EnablePathBindingSourceAutomaticValidation = true;

            // Enable validation for parameters bound from 'BindingSource.Custom' binding sources.
            configuration.EnableCustomBindingSourceAutomaticValidation = true;

            // Replace the default result factory with a custom implementation.
            configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
        });

        services.AddHttpContextAccessor();

        services.AddLazyCache();

        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtConfig:Issuer"],
                ValidAudience = configuration["JwtConfig:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:Secret"])),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
        });

        //Mapster global Setting
        TypeAdapterConfig.GlobalSettings.Default
                        .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase)
                        .IgnoreNullValues(true)
                        .AddDestinationTransform((string x) => x.Trim())
                        .AddDestinationTransform((string x) => x ?? "")
                        .AddDestinationTransform(DestinationTransform.EmptyCollectionIfNull);

        TypeAdapterConfig<Cart, CartItemsView>
               .NewConfig()
               .Map(dest => dest.AddedAt, src => src.AddedAt.ToLocalTime())
               .Map(dest => dest.UpdatedAt, src => src.UpdatedAt.ToLocalTime());

        services.AddSingleton<ICacheService, CacheService>();

        services.TryAddScoped<UserSession>();
        services.TryAddScoped<ITokenGenerator, TokenGenerator>();

        services.TryAddTransient<IAuthService, AuthService>();
        services.TryAddTransient<IBookService, BookService>();
        services.TryAddTransient<ICartService, CartService>();

        return services;
    }
}