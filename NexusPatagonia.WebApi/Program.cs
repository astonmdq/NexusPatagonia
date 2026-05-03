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
using NexusPatagonia.Mappings;
using AutoMapper;
using NexusPatagonia.Application.Interfaces;
using NexusPatagonia.Application.Services;
using NexusPatagonia.Middlewares;
using System.Text;
using NexusPatagonia.Infrastructure.Interfaces;
using NexusPatagonia.Infrastructure.Services.Authentication;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using NexusPatagonia.Infrastructure.Services.Reports;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
});

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

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);


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
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options =>
{ 
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "NexusPatagonia API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese 'Bearer' seguido de un espacio y luego su token JWT"

    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        // Opcional: Esto ayuda si moviste las entidades a otro proyecto (Domain)
        b => b.MigrationsAssembly("NexusPatagonia.Infrastructure")
    );
});
builder.Services.AddAuthorization();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IPdfProcessingStrategy, UthgraPdfStrategy>();
builder.Services.AddScoped<IPdfProcessingStrategy, DDJJPdfStrategy>();
builder.Services.AddScoped<IPdfProcessingStrategy, CPdfStrategy>();
builder.Services.AddScoped<IPdfProcessingStrategy, ReceiptsPdfStrategy>();
builder.Services.AddScoped<IPersistenceStrategy, ReceiptPersistenceStrategy>();
builder.Services.AddScoped<IPersistenceStrategy, ConceptPersistenceStrategy>();
builder.Services.AddScoped<IPersistenceStrategy, UthgraPersistenceStrategy>();
builder.Services.AddScoped<IPersistenceStrategy, DDJJPersistenceStrategy>();
builder.Services.AddScoped<IPersistenceCoordinator, PersistenceCoordinator>();
builder.Services.AddScoped<ICashMovementRepository, CashMovementRepository>();
builder.Services.AddScoped<ICashMovementService, CashMovementService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();  
builder.Services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPdfProcessingStrategy, TishPdfStrategy>();
builder.Services.AddScoped<IPersistenceStrategy, TishPersistenceStrategy>();
builder.Services.AddScoped<IProfitabilityReportRepository, ProfitabilityReportRepository>();
builder.Services.AddScoped<IProfitabilityService, ProfitabilityService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReceiptRepository, ReceiptRepository>();
builder.Services.AddScoped<IMonthlyConceptRepository, MonthlyConceptRepository>();
builder.Services.AddScoped<IConceptRepository, ConceptRepository>();
builder.Services.AddScoped<IProfitabilityService, ProfitabilityService>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ICheckingAccountRepository, CheckingAccountRepository>();
builder.Services.AddScoped<ITishRepository, TishRepository>();
builder.Services.AddScoped<IProfitabilityPdfReport, ProfitabilityPdfReport>();


builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowReactApp");




