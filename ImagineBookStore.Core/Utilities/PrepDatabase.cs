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
        Log.Information("--> Seeding Role Data...");

        //create default role data
        if (!context.Roles.Any())
        {
            context.Roles.AddRange(
                new Role { Name = nameof(Roles.Admin) },
                new Role { Name = nameof(Roles.User) }
                );
        }

        if (!context.Books.Any())
        {
            context.Books.AddRange(defaultBooks);
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
            }
            else
            {
                Log.Error("--> Adding first user failed: {0}", result.Message);
            }
        }
        catch (Exception ex)
        {
            Log.Error("--> Adding first user failed", ex);
        }
    }

    private static readonly List<Book> defaultBooks = new()
    {
        new Book
        {
            Title = "Clean Architecture: A Craftsman's Guide to Software Structure and Design (Robert C. Martin Series)",
            Author = "Robert C. Martin",
            Genre = "Software Architecture",
            TotalStock = 30,
            Price = 50.50m
        },
        new Book
        {
            Title = "Fundamentals of Software Architecture: An Engineering Approach",
            Author = "Mark Richards and Neal Ford",
            Genre = "Software Architecture",
            TotalStock = 20,
            Price = 54m
        },
        new Book
        {
            Title = "Become an Awesome Software Architect: Book 1: Foundation 2019",
            Author = "Anatoly Volkhover",
            Genre = "Arts & Photography",
            TotalStock = 19,
            Price = 21.9m
        },
        new Book
        {
            Title = "Bob's Burgers 2024 Wall Calendar",
            Author = "Twentieth Century Studios Inc",
            Genre = "Arts & Photography",
            TotalStock = 47,
            Price = 44m
        },
        new Book
        {
            Title = "Studio Ghibli: The Complete Works",
            Author = "Studio Ghibli",
            Genre = "Arts & Photography",
            TotalStock = 12,
            Price = 90m
        },
        new Book
        {
            Title = "Oath and Honor: A Memoir and a Warning",
            Author = "Liz Cheney",
            Genre = "History",
            TotalStock = 98,
            Price = 15m
        },
        new Book
        {
            Title = "Spare",
            Author = "Prince Harry The Duke of Sussex",
            Genre = "History",
            TotalStock = 54,
            Price = 5m
        },
        new Book
        {
            Title = "Oath and Honor: A Memoir and a Warning",
            Author = "Liz Cheney",
            Genre = "History",
            TotalStock = 37,
            Price = 109.5m
        },
        new Book
        {
            Title = "The Making of a King: King Charles III and the Modern Monarchy",
            Author = "Robert Hardman",
            Genre = "History",
            TotalStock = 12,
            Price = 10m
        },
        new Book
        {
            Title = "Hands of Time: A Watchmaker's History",
            Author = "Rebecca Struthers",
            Genre = "History",
            TotalStock = 98,
            Price = 11m
        },
        new Book
        {
            Title = "The Bluebook: A Uniform System of Citation",
            Author = "Columbia Law Review",
            Genre = "Law",
            TotalStock = 33,
            Price = 5.5m
        },
        new Book
        {
            Title = "The Accidental Superpower: Ten Years On",
            Author = "Mr. Peter Zeihan",
            Genre = "Law",
            TotalStock = 71,
            Price = 22m
        },
        new Book
        {
            Title = "Constitutional Law: [Connected eBook with Study Center] (Aspen Casebook)",
            Author = "Erwin Chemerinsky",
            Genre = "Law",
            TotalStock = 32,
            Price = 7m
        },
        new Book
        {
            Title = "The Art Of War",
            Author = "Sun Tzu",
            Genre = "Law",
            TotalStock = 81,
            Price = 20m
        },
        new Book
        {
            Title = "Open-Source Property: Spring 2024",
            Author = "Roger Allan Ford",
            Genre = "Law",
            TotalStock = 18,
            Price = 45m
        },
        new Book
        {
            Title = "The Loophole in LSAT Logical Reasoning",
            Author = "Ellen Cassidy",
            Genre = "Law",
            TotalStock = 26,
            Price = 10m
        },
        new Book
        {
            Title = "An Advocate Persuades",
            Author = "Joan Rocklin",
            Genre = "Law",
            TotalStock = 72,
            Price = 22m
        },
        new Book
        {
            Title = "American Government 3e",
            Author = "Glen Krutz",
            Genre = "Law",
            TotalStock = 53,
            Price = 88m
        },
        new Book
        {
            Title = "Into the Wild",
            Author = "Jon Krakauer",
            Genre = "Travel",
            TotalStock = 35,
            Price = 75m
        },
        new Book
        {
            Title = "Rand McNally Folded Map: United States Map",
            Author = "Rand McNally",
            Genre = "Travel",
            TotalStock = 11,
            Price = 10.5m
        },
        new Book
        {
            Title = "Elon Musk",
            Author = "Walter Isaacson",
            Genre = "Business & Money",
            TotalStock = 11,
            Price = 5.5m
        },
        new Book
        {
            Title = "How to Win Friends & Influence People",
            Author = "Dale Carnegie",
            Genre = "Business & Money",
            TotalStock = 57,
            Price = 7m
        },
        new Book
        {
            Title = "Rich Dad Poor Dad",
            Author = "Robert T. Kiyosaki",
            Genre = "Business & Money",
            TotalStock = 12,
            Price = 6.4m
        },
        new Book
        {
            Title = "The Laws of Human Nature",
            Author = "Robert Greene",
            Genre = "Business & Money",
            TotalStock = 90,
            Price = 10m
        }
    };
}