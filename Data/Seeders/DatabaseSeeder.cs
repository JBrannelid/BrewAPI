using BrewAPI.Data.ISeeders;
using BrewAPI.Models;
using BrewAPI.Settings;
using Microsoft.EntityFrameworkCore;

namespace BrewAPI.Data.Seeders
{
    // Seeder for creating Db with default data for test purpose
    public class DatabaseSeeder : IDatabaseSeeder
    {
        // Run all seeding methods
        public void SeedData(ModelBuilder modelBuilder)
        {
            SeedTables(modelBuilder);
            SeedUsers(modelBuilder);
            SeedCustomers(modelBuilder);
            SeedMenuItems(modelBuilder);
            SeedBookings(modelBuilder);
        }

        // Seed tables
        private static void SeedTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table>().HasData(
                new Table { PK_TableId = 1, TableNumber = 1, Capacity = 2, IsAvailable = true },
                new Table { PK_TableId = 2, TableNumber = 2, Capacity = 4, IsAvailable = true },
                new Table { PK_TableId = 3, TableNumber = 3, Capacity = 6, IsAvailable = true },
                new Table { PK_TableId = 4, TableNumber = 4, Capacity = 8, IsAvailable = true },
                new Table { PK_TableId = 5, TableNumber = 5, Capacity = 2, IsAvailable = true },
                new Table { PK_TableId = 6, TableNumber = 6, Capacity = 4, IsAvailable = true }
            );
        }

        // Seed users
        private static void SeedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "Johannes",
                    LastName = "Brannelid",
                    Email = "test@exempel.com",
                    Role = UserRole.Admin,
                    // Password: "admin123" hashed with BCrypt
                    PasswordHash = "$2a$11$8Xl3E5qDNqK0rV2QcMkV4eJ1Q8P7K9zF0nR6tY3sW2hA5cU1mB7dO"
                }
            );
        }

        // Seed customers
        private static void SeedCustomers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    PK_CustomerId = 1,
                    Name = "Test Testsson",
                    PhoneNumber = "070-1234567",
                    Email = "test@exempel.com"
                },
                new Customer
                {
                    PK_CustomerId = 2,
                    Name = "Tian Tiansson",
                    PhoneNumber = "076-1234567",
                    Email = "tian@exempel.com"
                }
            );
        }

        // Seed menu items. Items is taken from a real Café menu for realism
        // unsplash.com img for copyright reasons
        private static void SeedMenuItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem
                {
                    PK_MenuItemId = 1,
                    Name = "Ceasarsallad Räkor",
                    Category = "Salads",
                    Price = 145.00m,
                    Description = "Handskalade räkor, bacon, cocktailtomat, rostade kruttonger, picklad rödlök, grana padano",
                    IsPopular = true,
                    ImageUrl = "https://images.unsplash.com/photo-1512621776951-a57141f2eefd?w=400"
                },
                new MenuItem
                {
                    PK_MenuItemId = 2,
                    Name = "Chevré Salad",
                    Category = "Salads",
                    Price = 125.00m,
                    Description = "Rostade valnötter, fikon, rödbetor, äpple och rädisor, cocktailtomater, honung, ärtskott.",
                    IsPopular = false,
                    ImageUrl = "https://images.unsplash.com/photo-1540420773420-3366772f4999?w=400"
                },
                new MenuItem
                {
                    PK_MenuItemId = 3,
                    Name = "Dubai chocolate crêpe",
                    Category = "Desserts",
                    Price = 85.00m,
                    Description = "Fransk crêpe med pistagekräm, choklad och vaniljglass",
                    IsPopular = true,
                    ImageUrl = "https://images.unsplash.com/photo-1506459225024-1428097a7e18?w=400"
                },
                new MenuItem
                {
                    PK_MenuItemId = 4,
                    Name = "Cappuccino",
                    Category = "Beverages",
                    Price = 45.00m,
                    Description = "Krämig cappuccino med perfekt mjölkskum",
                    IsPopular = true,
                    ImageUrl = "https://images.unsplash.com/photo-1509042239860-f550ce710b93?w=400"
                },
                new MenuItem
                {
                    PK_MenuItemId = 5,
                    Name = "Tea Selection",
                    Category = "Beverages",
                    Price = 40.00m,
                    Description = "Grönt, svart eller rött té i olika smaker",
                    IsPopular = false,
                    ImageUrl = "https://images.unsplash.com/photo-1544787219-7f47ccb76574?w=400"
                }
            );
        }

        // Seed bookings
        private static void SeedBookings(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    PK_BookingId = 1,
                    FK_CustomerId = 1,
                    FK_TableId = 1,
                    BookingDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                    BookingTime = new TimeOnly(18, 30),
                    NumberGuests = 2,
                    DurationTime = TimeSpan.FromHours(BookingSettings.DefaultBookingDurationHours),
                    Status = "Confirmed"
                },
                new Booking
                {
                    PK_BookingId = 2,
                    FK_CustomerId = 2,
                    FK_TableId = 3,
                    BookingDate = DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
                    BookingTime = new TimeOnly(19, 0),
                    NumberGuests = 4,
                    DurationTime = TimeSpan.FromHours(BookingSettings.DefaultBookingDurationHours),
                    Status = "Confirmed"
                }
            );
        }
    }
}