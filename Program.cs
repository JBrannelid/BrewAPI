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

            var app = builder.Build();

            // Configure middleware pipeline and enable Swagger UI only in Development
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enforce HTTPS redirection
            app.UseHttpsRedirection();

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