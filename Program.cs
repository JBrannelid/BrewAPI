using BrewAPI.Extensions;

namespace BrewAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services
            builder.Services.AddDbServices(builder.Configuration);
            builder.Services.AddRepositories();
            builder.Services.AddServices();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerWithJwtSupport();
            builder.Services.AddJwtAuthenticationAndAuthorization(builder.Configuration);
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddDevelopmentCorsPolicy(builder.Configuration);
                // For future services specific to development environment
            }
            else
            {
                builder.Services.AddProductionCorsPolicy(builder.Configuration);
                // For future services specific to production environment
            }

            var app = builder.Build();

            // Configure middleware pipeline and enable Swagger UI only in Development
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enforce HTTPS redirection
            app.UseHttpsRedirection();

            // Configure CORS to allow requests from Frontend/React
            app.UseCors("AllowReactApp");

            // Enable Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Map API Controllers
            app.MapControllers();

            // Run the application
            app.Run();
        }
    }
}