using BrewAPI.Data.ISeeders;
using BrewAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BrewAPI.Data
{
    // Application database context
    public class BrewAPIDbContext : DbContext
    {
        // Inject seeding data when creating the DB context through DI
        private readonly IDatabaseSeeder _databaseSeeder;

        public BrewAPIDbContext(DbContextOptions<BrewAPIDbContext> options, IDatabaseSeeder databaseSeeder)
            : base(options)
        {
            _databaseSeeder = databaseSeeder;
        }

        // DbSets for application entities
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<User> Users { get; set; }

        // Configure entities and seed data with fluid API for long-term maintainability
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUserEntity(modelBuilder);
            ConfigureTableEntity(modelBuilder);
            ConfigureCustomerEntity(modelBuilder);
            ConfigureBookingEntity(modelBuilder);
            ConfigureMenuItemEntity(modelBuilder);

            // Seed initial data
            _databaseSeeder.SeedData(modelBuilder);
        }

        // Table entity configuration
        private static void ConfigureTableEntity(ModelBuilder modelBuilder)
        {
            var tableEntity = modelBuilder.Entity<Table>();
            tableEntity.HasKey(e => e.Id); 
            tableEntity.HasIndex(e => e.TableNumber).IsUnique();
            tableEntity.Property(e => e.TableNumber).IsRequired();
            tableEntity.Property(e => e.Capacity).IsRequired();
            tableEntity.Property(e => e.IsAvailable).HasDefaultValue(true);
        }

        // Customer entity configuration
        private static void ConfigureCustomerEntity(ModelBuilder modelBuilder)
        {
            var customerEntity = modelBuilder.Entity<Customer>();
            customerEntity.HasKey(e => e.Id);
            customerEntity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            customerEntity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
            customerEntity.HasIndex(e => e.PhoneNumber).IsUnique();
            customerEntity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            customerEntity.HasIndex(e => e.Email).IsUnique();
        }

        // Booking entity configuration
        private static void ConfigureBookingEntity(ModelBuilder modelBuilder)
        {
            var bookingEntity = modelBuilder.Entity<Booking>();
            bookingEntity.HasKey(e => e.Id);
            bookingEntity.Property(e => e.BookingDate).IsRequired();
            bookingEntity.Property(e => e.BookingTime).IsRequired();
            bookingEntity.Property(e => e.NumberGuests).IsRequired();
            bookingEntity.Property(e => e.Status).HasDefaultValue("Confirmed").HasMaxLength(20);
            bookingEntity.HasOne(e => e.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(e => e.FK_CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            bookingEntity.HasOne(e => e.Table)
                .WithMany(t => t.Bookings)
                .HasForeignKey(e => e.FK_TableId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        // MenuItem entity configuration
        private static void ConfigureMenuItemEntity(ModelBuilder modelBuilder)
        {
            var menuItemEntity = modelBuilder.Entity<MenuItem>();
            menuItemEntity.HasKey(e => e.Id);
            menuItemEntity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            menuItemEntity.Property(e => e.Category).HasMaxLength(50);
            menuItemEntity.Property(e => e.Price).IsRequired().HasColumnType("decimal(7,2)");
            menuItemEntity.Property(e => e.Description).HasMaxLength(500);
            menuItemEntity.Property(e => e.IsPopular).HasDefaultValue(false);
            menuItemEntity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasDefaultValue("https://plus.unsplash.com/premium_photo-1661349883108-3aea72f4a83f?w=900&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8ZW1wdHklMjBwbGF0ZXxlbnwwfHwwfHx8MA%3D%3D"); 
        }

        // User entity configuration
        private static void ConfigureUserEntity(ModelBuilder modelBuilder)
        {
            var userEntity = modelBuilder.Entity<User>();
            userEntity.HasKey(u => u.Id);
            userEntity.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            userEntity.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);
            userEntity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);
            userEntity.HasIndex(u => u.Email).IsUnique();
            userEntity.Property(u => u.Role)
                .HasConversion<string>()
                .HasMaxLength(20);
            userEntity.Property(u => u.PasswordHash)
                .IsRequired();
        }
    }
}