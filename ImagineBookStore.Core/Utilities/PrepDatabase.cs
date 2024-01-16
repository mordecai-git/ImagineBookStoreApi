using ImagineBookStore.Core.Models.App;
using ImagineBookStore.Core.Models.App.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ImagineBookStore.Core.Utilities;

public static class PrepDatabase
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<BookStoreContext>());
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
}