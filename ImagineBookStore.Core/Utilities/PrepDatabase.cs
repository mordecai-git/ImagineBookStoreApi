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
        using var serviceScope = app.ApplicationServices.CreateScope();

        SeedData(serviceScope.ServiceProvider.GetService<BookStoreContext>());

        await CreateFirstUserAsync(serviceScope.ServiceProvider.GetService<IAuthService>());
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

        // add default books
        if (!context.Books.Any())
        {
            context.Books.AddRange(defaultBooks);
        }

        // add default cart for default user
        if (!context.Carts.Any())
        {
            context.Carts.AddRange(defaultCarts);
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
            Amount = 5000.50m
        },
        new Book
        {
            Title = "Fundamentals of Software Architecture: An Engineering Approach",
            Author = "Mark Richards and Neal Ford",
            Genre = "Software Architecture",
            TotalStock = 20,
            Amount = 5400m
        },
        new Book
        {
            Title = "Become an Awesome Software Architect: Book 1: Foundation 2019",
            Author = "Anatoly Volkhover",
            Genre = "Arts & Photography",
            TotalStock = 19,
            Amount = 2100.9m
        },
        new Book
        {
            Title = "Bob's Burgers 2024 Wall Calendar",
            Author = "Twentieth Century Studios Inc",
            Genre = "Arts & Photography",
            TotalStock = 47,
            Amount = 4400m
        },
        new Book
        {
            Title = "Studio Ghibli: The Complete Works",
            Author = "Studio Ghibli",
            Genre = "Arts & Photography",
            TotalStock = 12,
            Amount = 9000m
        },
        new Book
        {
            Title = "Oath and Honor: A Memoir and a Warning",
            Author = "Liz Cheney",
            Genre = "History",
            TotalStock = 98,
            Amount = 1500m
        },
        new Book
        {
            Title = "Spare",
            Author = "Prince Harry The Duke of Sussex",
            Genre = "History",
            TotalStock = 54,
            Amount = 500m
        },
        new Book
        {
            Title = "Oath and Honor: A Memoir and a Warning",
            Author = "Liz Cheney",
            Genre = "History",
            TotalStock = 37,
            Amount = 10900.5m
        },
        new Book
        {
            Title = "The Making of a King: King Charles III and the Modern Monarchy",
            Author = "Robert Hardman",
            Genre = "History",
            TotalStock = 12,
            Amount = 1000m
        },
        new Book
        {
            Title = "Hands of Time: A Watchmaker's History",
            Author = "Rebecca Struthers",
            Genre = "History",
            TotalStock = 98,
            Amount = 1100m
        },
        new Book
        {
            Title = "The Bluebook: A Uniform System of Citation",
            Author = "Columbia Law Review",
            Genre = "Law",
            TotalStock = 33,
            Amount = 500.5m
        },
        new Book
        {
            Title = "The Accidental Superpower: Ten Years On",
            Author = "Mr. Peter Zeihan",
            Genre = "Law",
            TotalStock = 71,
            Amount = 2200m
        },
        new Book
        {
            Title = "Constitutional Law: [Connected eBook with Study Center] (Aspen Casebook)",
            Author = "Erwin Chemerinsky",
            Genre = "Law",
            TotalStock = 32,
            Amount = 700m
        },
        new Book
        {
            Title = "The Art Of War",
            Author = "Sun Tzu",
            Genre = "Law",
            TotalStock = 81,
            Amount = 2000m
        },
        new Book
        {
            Title = "Open-Source Property: Spring 2024",
            Author = "Roger Allan Ford",
            Genre = "Law",
            TotalStock = 18,
            Amount = 4500m
        },
        new Book
        {
            Title = "The Loophole in LSAT Logical Reasoning",
            Author = "Ellen Cassidy",
            Genre = "Law",
            TotalStock = 26,
            Amount = 1000m
        },
        new Book
        {
            Title = "An Advocate Persuades",
            Author = "Joan Rocklin",
            Genre = "Law",
            TotalStock = 72,
            Amount = 2200m
        },
        new Book
        {
            Title = "American Government 3e",
            Author = "Glen Krutz",
            Genre = "Law",
            TotalStock = 53,
            Amount = 8800m
        },
        new Book
        {
            Title = "Into the Wild",
            Author = "Jon Krakauer",
            Genre = "Travel",
            TotalStock = 35,
            Amount = 7500m
        },
        new Book
        {
            Title = "Rand McNally Folded Map: United States Map",
            Author = "Rand McNally",
            Genre = "Travel",
            TotalStock = 11,
            Amount = 1000.5m
        },
        new Book
        {
            Title = "Elon Musk",
            Author = "Walter Isaacson",
            Genre = "Business & Money",
            TotalStock = 11,
            Amount = 500.5m
        },
        new Book
        {
            Title = "How to Win Friends & Influence People",
            Author = "Dale Carnegie",
            Genre = "Business & Money",
            TotalStock = 57,
            Amount = 700m
        },
        new Book
        {
            Title = "Rich Dad Poor Dad",
            Author = "Robert T. Kiyosaki",
            Genre = "Business & Money",
            TotalStock = 12,
            Amount = 600.4m
        },
        new Book
        {
            Title = "The Laws of Human Nature",
            Author = "Robert Greene",
            Genre = "Business & Money",
            TotalStock = 90,
            Amount = 1000m
        }
    };

    private static readonly List<Cart> defaultCarts = new()
    {
        new Cart
        {
            UserId = 1,
            BookId = 3,
            Quantity = 2
        },
        new Cart
        {
            UserId = 1,
            BookId = 7,
            Quantity = 3
        },
        new Cart
        {
            UserId = 1,
            BookId = 9,
            Quantity = 5
        }
    };
}