using System.Text;
using backend.Business.src.Common;
using backend.Business.src.Implementations;
using backend.Business.src.Interfaces;
using backend.Domain.src.RepoInterfaces;
using backend.WebApi.src.AuthorizationRequirements;
using backend.WebApi.src.Database;
using backend.WebApi.src.Middlewares;
using backend.WebApi.src.RepoImplementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

//Add AutoMapper DI
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//Add database
builder.Services.AddDbContext<DatabaseContext>();

//Add Service DI
builder.Services
    .AddScoped<IUserRepo, UserRepo>()
    .AddScoped<IUserService, UserService>()
    .AddScoped<IProductRepo, ProductRepo>()
    .AddScoped<IProductService, ProductService>()
    .AddScoped<IOrderRepo, OrderRepo>()
    .AddScoped<IOrderService, OrderService>()
    .AddScoped<IOrderItemRepo, OrderItemRepo>()
    .AddScoped<IOrderItemService, OrderItemService>()
    .AddScoped<IImageRepo, ImageRepo>()
    .AddScoped<IImageService, ImageService>()
    .AddScoped<IAuthService, AuthService>()
    .AddScoped<IPasswordService, PasswordService>()
    .AddScoped<IReviewRepo,ReviewRepo>()
    .AddScoped<IReviewService, ReviewService>()
    .AddScoped<ICategoryRepo,CategoryRepo>()
    .AddScoped<ICategoryService, CategoryService>();

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "oauth2",
        new OpenApiSecurityScheme
        {
            Description = "Bearer token authentication",
            Name = "Authentication",
            In = ParameterLocation.Header
        }
    );
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//add policy based requirement handler
builder.Services
.AddSingleton<ErrorHandlerMiddleware>()
.AddSingleton<ResourceOwnerAuthorization>();

//Config route
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

//Config the authentication
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "backend",
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("my-secret-key-is-unique-and-should-keep-it-safe")
            ),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        "OwnerOnly",
        policy => policy.Requirements.Add(new ResourceOwnerAuthorization())
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
