using Microsoft.EntityFrameworkCore;

namespace BrewAPI.Data.ISeeders
{
    // Interface for database seeding
    public interface IDatabaseSeeder
    {
        // Void method to seed initial data into the database
        void SeedData(ModelBuilder modelBuilder);
    }
}