using Microsoft.OpenApi.Models;
using ImagineBookStore.Core.Extensions;
using Serilog;
using ImagineBookStore.Core.Middlewares;
using ImagineBookStore.Core.Models.Configurations;
using ImagineBookStore.Core.Utilities;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Application Starting up...");

var builder = WebApplication.CreateBuilder(args);

// set up serilog.
builder.Host.UseSerilog((hostContext, services, config) =>
{
    config.ReadFrom.Configuration(hostContext.Configuration);
    config.ReadFrom.Services(services);
    config.Enrich.FromLogContext();
    config.WriteTo.Console();
});


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Imagine Book Store API",
        Description = "Imagine Book Store Web API. A project for managing and tracking a book store.",
        Contact = new OpenApiContact
        {
            Name = "Mordecai Godwin",
            Email = "davidire71@gmail.com"
        }
    });

    string xmlFilePath = Path.Combine(AppContext.BaseDirectory, "ImagineBookStoreApi.xml");
    swagger.IncludeXmlComments(xmlFilePath, true);

    // include the XML of Imagine Book Store.Core
    string coreXmlFilePath = Path.Combine(AppContext.BaseDirectory, "ImagineBookStoreCore.xml");
    swagger.IncludeXmlComments(coreXmlFilePath, true);

    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
    });

    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    swagger.IgnoreObsoleteActions();
    swagger.IgnoreObsoleteProperties();
});

// set up app settings helpers
builder.Services.Configure<AppConfig>(builder.Configuration.GetSection("AppConfig"));
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

// build external services
builder.Services.ConfigureServices(builder.Configuration, builder.Environment.IsProduction());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

PrepDatabase.PrepPopulation(app);

app.Run();

Log.Information("Application Stopped cleanly");

Log.CloseAndFlush();