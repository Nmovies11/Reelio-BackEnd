using BLL.Services;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using DAL.Repositories;
using DAL.API.Repositories;
using Microsoft.EntityFrameworkCore;
using API.Middleware;
using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IShowRepository, ShowRepository>();
builder.Services.AddScoped<IShowService, ShowService>();

if (builder.Environment.IsProduction())
{
    var keyVaultURL = builder.Configuration.GetValue<string>("KeyVault:KeyvaultURL");
    var credential = new ManagedIdentityCredential();

    var secretClient = new SecretClient(new Uri(keyVaultURL), credential);

    builder.Configuration.AddAzureKeyVault(secretClient, new AzureKeyVaultConfigurationOptions());

    var connectionString = builder.Configuration["DatabaseURLProd"];

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Database connection string 'DatabaseURLProd' is missing.");
    }

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseMySql(
            connectionString,
            new MySqlServerVersion(new Version(8, 0, 39)),
            mysqlOptions => mysqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,        
            maxRetryDelay: TimeSpan.FromSeconds(10), 
            errorNumbersToAdd: null  
        )

        );
    });
}

if(builder.Environment.IsStaging())
{
    var keyVaultURL = builder.Configuration.GetValue<string>("KeyVault:KeyvaultURL");
    var credential = new ManagedIdentityCredential();

    var secretClient = new SecretClient(new Uri(keyVaultURL), credential);

    builder.Configuration.AddAzureKeyVault(secretClient, new AzureKeyVaultConfigurationOptions());

    var connectionString = builder.Configuration["DatabaseURLStaging"];

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Database connection string 'DatabaseURLStaging' is missing.");
    }

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseMySql(
            connectionString,
            new MySqlServerVersion(new Version(8, 0, 39)),
            mysqlOptions => mysqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null
        ));
    });
}



if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseMySql(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
    });
}

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                "https://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});



var app = builder.Build();
app.UseCors();

app.UseMiddleware<AuthenticationMiddleware>();




    app.UseSwagger();
    app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}



app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();



app.Run();
