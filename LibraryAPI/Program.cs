using Library.Business;
using Library.DataAccess;
using Library.Repositories;
using LibraryAPI.Endpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LibraryAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDb")));

var jwtKey = builder.Configuration["Jwt:Key"];

builder.Services
    .AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
            };
    });


builder.Services.AddRepositories();
builder.Services.AddBusiness();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("/api/users").MapUserEndpoints().WithTags("Users");
app.MapGroup("/api/auth").MapAuthEndpoints().WithTags("Auth");
app.MapGroup("/api/loans").MapLoanEndpoints().WithTags("Loans");
app.MapGroup("/api/authors").MapAuthorEndpoints().WithTags("Authors");
app.MapGroup("/api/categories").MapCategoryEndpoints().WithTags("Categories");
app.MapGroup("/api/books").MapBookEndpoints().WithTags("Books");
app.MapGroup("/api/readers").MapReaderEndpoints().WithTags("Readers");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
