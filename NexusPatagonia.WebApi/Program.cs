using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;
using NexusPatagonia.Infrastructure.Services.Pdf;
using NexusPatagonia.Infrastructure.Services.Persistence;
using NexusPatagonia.Infrastructure.Services.Strategies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        // Opcional: Esto ayuda si moviste las entidades a otro proyecto (Domain)
        b => b.MigrationsAssembly("NexusPatagonia.Infrastructure")
    );
});

builder.Services.AddScoped<IPdfProcessingStrategy, UthgraPdfStrategy>();
builder.Services.AddScoped<IPdfProcessingStrategy, DDJJPdfStrategy>();
builder.Services.AddScoped<IPdfProcessingStrategy, CPdfStrategy>();
builder.Services.AddScoped<IPdfProcessingStrategy, ReceiptsPdfStrategy>();
builder.Services.AddScoped<IPersistenceStrategy, ReceiptPersistenceStrategy>();
builder.Services.AddScoped<IPersistenceStrategy, ConceptPersistenceStrategy>();
builder.Services.AddScoped<IPersistenceStrategy, UthgraPersistenceStrategy>();
builder.Services.AddScoped<IPersistenceStrategy, DDJJPersistenceStrategy>();
builder.Services.AddScoped<IPersistenceCoordinator, PersistenceCoordinator>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

