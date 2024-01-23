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

        // set up Paystack HttpClient
        string paystackHttpClientName = configuration["Paystack:HttpClientName"];
        ArgumentException.ThrowIfNullOrEmpty(paystackHttpClientName);

        string paystackKey = configuration["Paystack:Key"];
        ArgumentException.ThrowIfNullOrEmpty(paystackKey);

        services.AddHttpClient(
            paystackHttpClientName,
            client =>
            {
                // Set the base address of the named client.
                client.BaseAddress = new Uri("https://api.paystack.co/");

                // Add a user-agent default request header.
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", paystackKey);
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
               .Map(dest => dest.CreatedAt, src => src.CreatedAt.ToLocalTime())
               .Map(dest => dest.UpdatedAt, src => src.UpdatedAt.ToLocalTime())
               .Map(dest => dest.IsStillAvailable, src => src.Book.TotalStock >= src.Quantity);

        TypeAdapterConfig<Order, OrderView>
               .NewConfig()
               .Map(dest => dest.CreatedAt, src => src.CreatedAt.ToLocalTime())
               .Map(dest => dest.UpdatedAt, src => src.UpdatedAt.ToLocalTime());

        services.AddSingleton<ICacheService, CacheService>();

        services.TryAddScoped<UserSession>();
        services.TryAddScoped<ITokenGenerator, TokenGenerator>();

        services.TryAddTransient<IAuthService, AuthService>();
        services.TryAddTransient<IBookService, BookService>();
        services.TryAddTransient<ICartService, CartService>();
        services.TryAddTransient<IOrderService, OrderService>();

        return services;
    }
}