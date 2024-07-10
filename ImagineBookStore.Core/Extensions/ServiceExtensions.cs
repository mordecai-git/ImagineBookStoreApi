using FluentValidation;
using ImagineBookStore.Core.Interfaces;
using ImagineBookStore.Core.Services;
using ImagineBookStore.Model.App;
using ImagineBookStore.Model.Input;
using ImagineBookStore.Model.View;
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

/// <summary>
/// Extension methods for configuring services in the application.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Configures services for the application, including database, validation, authentication, authorization, HTTP context, caching, and various services.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configuration">The configuration for the application.</param>
    /// <param name="isProduction">A flag indicating whether the application is running in a production environment.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> for method chaining.</returns>
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration, bool isProduction)
    {
        // Add in-memory database for BookStoreContext
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

        // Add HTTP context accessor
        services.AddHttpContextAccessor();

        // Add LazyCache for caching
        services.AddLazyCache();

        // Add authentication using JWT Bearer scheme
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

        // Add authorization with fallback policy
        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
        });

        // Set up Paystack HttpClient
        string paystackHttpClientName = configuration["Paystack:HttpClientName"];
        ArgumentException.ThrowIfNullOrEmpty(paystackHttpClientName);

        string paystackKey = configuration["Paystack:Key"];
        ArgumentException.ThrowIfNullOrEmpty(paystackKey);

        // Configure Paystack HttpClient
        services.AddHttpClient(
            paystackHttpClientName,
            client =>
            {
                // Set the base address of the named client.
                client.BaseAddress = new Uri("https://api.paystack.co/");

                // Add a user-agent default request header.
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", paystackKey);
            });

        // Mapster global Setting
        TypeAdapterConfig.GlobalSettings.Default
                        .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase)
                        .IgnoreNullValues(true)
                        .AddDestinationTransform((string x) => x.Trim())
                        .AddDestinationTransform((string x) => x ?? "")
                        .AddDestinationTransform(DestinationTransform.EmptyCollectionIfNull);

        // Mapster configuration for Cart and CartItemsView
        TypeAdapterConfig<Cart, CartItemsView>
               .NewConfig()
               .Map(dest => dest.CreatedAt, src => src.CreatedAt.ToLocalTime())
               .Map(dest => dest.UpdatedAt, src => src.UpdatedAt.ToLocalTime())
               .Map(dest => dest.IsStillAvailable, src => src.Book.TotalStock >= src.Quantity);

        // Mapster configuration for Order and OrderView
        TypeAdapterConfig<Order, OrderView>
               .NewConfig()
               .Map(dest => dest.CreatedAt, src => src.CreatedAt.ToLocalTime())
               .Map(dest => dest.UpdatedAt, src => src.UpdatedAt.ToLocalTime());

        // Add CacheService as a singleton
        services.AddSingleton<ICacheService, CacheService>();

        // Add UserSession and TokenGenerator as scoped services
        services.TryAddScoped<UserSession>();
        services.TryAddScoped<ITokenGenerator, TokenGenerator>();

        // Add application services
        services.TryAddTransient<IAuthService, AuthService>();
        services.TryAddTransient<IBookService, BookService>();
        services.TryAddTransient<ICartService, CartService>();
        services.TryAddTransient<IOrderService, OrderService>();

        return services;
    }
}