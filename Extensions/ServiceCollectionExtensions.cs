using BrewAPI.Data;
using BrewAPI.Data.ISeeders;
using BrewAPI.Data.Seeders;
using BrewAPI.Repositories;
using BrewAPI.Repositories.IRepositories;
using BrewAPI.Services;
using BrewAPI.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BrewAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // Add DbContext and configure SQL Server + Database Seeder
        public static IServiceCollection AddDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DbContext and configure SQL Server
            services.AddDbContext<BrewAPIDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // Database Seeder
            services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();

            return services;
        }

        // Add generic and specifik repositories
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Generic repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // Specific repositories
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        // Add Services
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IMenuItemService, MenuItemService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtService, JwtService>();

            return services;
        }

        // Add Swagger/OpenAPI support + Configure Swagger for JWT authentication (Bearer)
        public static IServiceCollection AddSwaggerWithJwtSupport(this IServiceCollection services)
        {
            // Add Swagger/OpenAPI support
            services.AddEndpointsApiExplorer();

            // Configure Swagger for JWT authentication (Bearer)
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference { Id = "Bearer", Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme }
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }

        // Configure JWT Authentication and Authorization policies
        public static IServiceCollection AddJwtAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure JWT Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Token validation settings
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            // Configure Authorization policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOrManager", policy =>
                    policy.RequireRole("Admin", "Manager"));
            });

            return services;
        }

        // Add production Cors. Look through in production how to configure it properly
        public static IServiceCollection AddProductionCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod() // HTTP methods (GET, POST, etc.)
                          .AllowCredentials(); // Allow credentials for JWT tokens
                });
            });
            return services;
        }

        // Add dev Cors. Look through in production how to configure it properly
        public static IServiceCollection AddDevelopmentCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

                        policy.WithOrigins(allowedOrigins)
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials(); // Allow credentials for JWT tokens
                });
            });
            return services;
        }
    }
}