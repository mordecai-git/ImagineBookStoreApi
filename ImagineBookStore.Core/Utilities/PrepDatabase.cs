using ImagineBookStore.Core.Interfaces;
using ImagineBookStore.Core.Models.App;
using ImagineBookStore.Core.Models.App.Constants;
using ImagineBookStore.Core.Models.Input;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ImagineBookStore.Core.Utilities;

public static class PrepDatabase
{
    public static async void PrepPopulation(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<BookStoreContext>());

            await CreateFirstUserAsync(serviceScope.ServiceProvider.GetService<IAuthService>());
        }
    }

    private static void SeedData(BookStoreContext context)
    {
        //create default role data
        if (!context.Roles.Any())
        {
            Log.Information("--> Seeding Role Data...");

            context.Roles.AddRange(
                new Role { Name = nameof(Roles.Admin) },
                new Role { Name = nameof(Roles.User) }
                );
        }

        context.SaveChanges();
    }

    private static async Task CreateFirstUserAsync(IAuthService authService)
    {
        Log.Information("--> Adding First User");

        try
        {
            var newUser = new RegisterModel
            {
                Email = "jane@doe.com",
                FirstName = "Jane",
                LastName = "Doe",
                Password = "Password1@",
                ConfirmPassword = "Password1@"
            };

            var result = await authService.CreateUser(newUser);

            if (result.Success)
            {
                Log.Information("--> Added first user successfully. Email: {0}; Password: {1}", newUser.Email, newUser.Password);
            } else
            {
                Log.Error("--> Adding first user failed: {0}", result.Message);
            }
        }
        catch (Exception ex)
        {
            Log.Error("--> Adding first user failed", ex);
        }
    }
}