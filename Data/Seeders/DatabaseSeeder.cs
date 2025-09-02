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

        // Seed menu items (unsplash.com img for copyright reasons)
        private static void SeedMenuItems(ModelBuilder modelBuilder)
        {
            // Salads
            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem
                {
                    PK_MenuItemId = 1,
                    Name = "Ceasarsallad Räkor",
                    Category = "Salads",
                    Price = 145.00m,
                    Description = "Handskalade räkor, bacon, cocktailtomat, rostade kruttonger, picklad rödlök, grana padano",
                    IsPopular = true,
                    ImageUrl = "https://images.unsplash.com/photo-1551248429-40975aa4de74?w=400"
                },
                new MenuItem
                {
                    PK_MenuItemId = 2,
                    Name = "Ceasarsallad",
                    Category = "Salads",
                    Price = 145.00m,
                    Description = "Svensk kyckling, caesar dressing, rödlök, ugnsbakade cocktailtomater, parmesan, & egenrostade krutonger",
                    IsPopular = true,
                    ImageUrl = "https://images.unsplash.com/photo-1546793665-c74683f339c1?fm=jpg&q=60&w=3000&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D?w=400"
                },
                new MenuItem
                {
                    PK_MenuItemId = 3,
                    Name = "Tonfisksallad",
                    Category = "Salads",
                    Price = 145.00m,
                    Description = "Halstrad tonfiskfilé med sallad citron & korianderkräm",
                    IsPopular = false,
                    ImageUrl = "https://images.unsplash.com/photo-1604909052743-94e838986d24?q=80&w=1160&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D?w=400"
                },
                new MenuItem
                {
                    PK_MenuItemId = 4,
                    Name = "Chevré Salad",
                    Category = "Salads",
                    Price = 125.00m,
                    Description = "Rostade valnötter, fikon, rödbetor, äpple och rädisor, cocktailtomater, honung, ärtskott.",
                    IsPopular = false,
                    ImageUrl = "https://images.unsplash.com/photo-1540420773420-3366772f4999?w=400"
                },
                // Bowls
                new MenuItem
                {
                    PK_MenuItemId = 5,
                    Name = "Din-Din Bap Bowl",
                    Category = "Bowls",
                    Price = 188.00m,
                    Description = "Bulgogimarinerat högrev, bakat ägg, kimchi, morötter, spenat, böngroddar, furikake, sesamfrön, gochujangsås, sojamayo",
                    IsPopular = true,
                    ImageUrl = "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?q=80&w=1160&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new MenuItem
                {
                    PK_MenuItemId = 6,
                    Name = "Sneaky Samurai Bowl",
                    Category = "Bowls",
                    Price = 164.00m,
                    Description = "Kycklinglårfilé, ris, mango, teriyaksås, sojamajo, salladslök, togarashi, chili, soja, avokado, sojabönor, kimchi, picklad rödkål, smashed spicy cucumber",
                    IsPopular = false,
                    ImageUrl = "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?q=80&w=1160&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new MenuItem
                {
                    PK_MenuItemId = 7,
                    Name = "Karaage Bowl",
                    Category = "Bowls",
                    Price = 164.00m,
                    Description = "Kycklinglårfilé, ris, mango, teriyaksås, sojamajo, salladslök, togarashi, chili, soja, avokado, sojabönor, kimchi, picklad rödkål, smashed spicy cucumber",
                    IsPopular = false,
                    ImageUrl = "https://plus.unsplash.com/premium_photo-1705056547195-a68c45f2d77e?w=900&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8Zm9vZCUyMGJvd2x8ZW58MHx8MHx8fDA%3D"
                },

                // Deserts
                new MenuItem
                {
                    PK_MenuItemId = 8,
                    Name = "Dubai chocolate crêpe",
                    Category = "Desserts",
                    Price = 85.00m,
                    Description = "Fransk crêpe med pistagekräm, choklad och vaniljglass",
                    IsPopular = true,
                    ImageUrl = "https://images.unsplash.com/photo-1723691802547-b79f65c16b5f?q=80&w=1740&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new MenuItem
                {
                    PK_MenuItemId = 9,
                    Name = "Croissant",
                    Category = "Desserts",
                    Price = 45.00m,
                    Description = "Frasig croissant bakad med smör",
                    IsPopular = false,
                    ImageUrl = "https://images.unsplash.com/photo-1723691802547-b79f65c16b5f?q=80&w=1740&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new MenuItem
                {
                    PK_MenuItemId = 10,
                    Name = "Pumpkin Bun",
                    Category = "Desserts",
                    Price = 45.00m,
                    Description = "Vetebulle bakad med surdeg, fylld med pumpafyllning och toppad med majssmulor.",
                    IsPopular = false,
                    ImageUrl = "https://images.unsplash.com/photo-1589783361701-d5161a106415?q=80&w=1740&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3DY"
                },
                new MenuItem
                {
                    PK_MenuItemId = 11,
                    Name = "Mudcake",
                    Category = "Desserts",
                    Price = 65.00m,
                    Description = "Kladdkaka som serveras med vispgrädde och vaniljglass",
                    IsPopular = true,
                    ImageUrl = "https://images.unsplash.com/photo-1705472017435-7a820b01f36c?q=80&w=1740&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new MenuItem
                {
                    PK_MenuItemId = 12,
                    Name = "Moussekaka Cookies & Cream",
                    Category = "Desserts",
                    Price = 65.00m,
                    Description = "En rund kak- och gräddmousse med chokladsmulor",
                    IsPopular = true,
                    ImageUrl = "https://plus.unsplash.com/premium_photo-1695028377713-f5e5424b1e7e?q=80&w=774&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },


                // Beverages
                new MenuItem
                {
                    PK_MenuItemId = 13,
                    Name = "Cappuccino",
                    Category = "Beverages",
                    Price = 45.00m,
                    Description = "Krämig cappuccino med perfekt mjölkskum",
                    IsPopular = false,
                    ImageUrl = "https://images.unsplash.com/photo-1509042239860-f550ce710b93?w=400"
                },

                new MenuItem
                {
                    PK_MenuItemId = 14,
                    Name = "Ismatcha",
                    Category = "Beverages",
                    Price = 45.00m,
                    Description = "Sötat grönt matchate, serverat med kall havredryck och is",
                    IsPopular = true,
                    ImageUrl = "https://images.unsplash.com/photo-1717603545758-88cc454db69b?q=80&w=774&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new MenuItem
                {
                    PK_MenuItemId = 15,
                    Name = "Iced Latte",
                    Category = "Beverages",
                    Price = 45.00m,
                    Description = "Espresso blandat med kall mjölk och is",
                    IsPopular = false,
                    ImageUrl = "https://images.unsplash.com/photo-1517701550927-30cf4ba1dba5?q=80&w=774&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new MenuItem
                {
                    PK_MenuItemId = 16,
                    Name = "Latte",
                    Category = "Beverages",
                    Price = 45.00m,
                    Description = "Espresso blandat med mjölk",
                    IsPopular = false,
                    ImageUrl = "https://images.unsplash.com/photo-1574914629385-46448b767aec?q=80&w=774&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new MenuItem
                {
                    PK_MenuItemId = 17,
                    Name = "Hot Coffee",
                    Category = "Beverages",
                    Price = 45.00m,
                    Description = "Blonde Roast - Veranda Blend",
                    IsPopular = false,
                    ImageUrl = "https://images.unsplash.com/photo-1610632380989-680fe40816c6?q=80&w=774&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new MenuItem
                {
                    PK_MenuItemId = 18,
                    Name = "Caramel Frappuccino",
                    Category = "Beverages",
                    Price = 45.00m,
                    Description = "Vaniljshake med smak av choklad. Toppad med vispgrädde",
                    IsPopular = true,
                    ImageUrl = "https://images.unsplash.com/photo-1572490122747-3968b75cc699?q=80&w=774&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new MenuItem
                {
                    PK_MenuItemId = 19,
                    Name = "Caramel Frappuccino",
                    Category = "Beverages",
                    Price = 45.00m,
                    Description = "Kaffeshake med smak av kola. Toppad med vispgrädde",
                    IsPopular = false,
                    ImageUrl = "https://images.unsplash.com/photo-1637178035222-a08f2d4dd1a3?q=80&w=756&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"

                },
                new MenuItem
                {
                    PK_MenuItemId = 20,
                    Name = "Tea Selection",
                    Category = "Beverages",
                    Price = 40.00m,
                    Description = "Grönt, svart eller rött té i olika smaker",
                    IsPopular = true,
                    ImageUrl = "https://plus.unsplash.com/premium_photo-1674406481284-43eba097a291?q=80&w=1740&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
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
                    BookingDate = new DateOnly(2025, 9, 15), 
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
                    BookingDate = new DateOnly(2025, 9, 16), 
                    BookingTime = new TimeOnly(19, 0),
                    NumberGuests = 4,
                    DurationTime = TimeSpan.FromHours(BookingSettings.DefaultBookingDurationHours),
                    Status = "Confirmed"
                }
            );
        }
    }
}