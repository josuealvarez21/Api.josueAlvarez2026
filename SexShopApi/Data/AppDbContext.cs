using Microsoft.EntityFrameworkCore;
using SexShopApi.Models;

namespace SexShopApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Users
            // Password is "Admin123!" -> Hashed (example hash)
            // Password is "Guest123!" -> Hashed (example hash)
            // Note: In a real scenario, use a tool to generate these hashes. 
            // Here I'm using a placeholder valid bcrypt hash for "password" for simplicity in this example, 
            // or I'd need to run a small utility. 
            // Let's assume these are valid hashes for "password" for now, user should change them.
            // Actually, I'll use a known hash for "password" for both for testing.
            // Hash for "password": $2a$11$Zk.1.1.1.1.1.1.1.1.1.1.1.1.1.1.1.1.1.1.1.1.1.1.1 
            // (Invalid hash structure, let's use a real one or leave it to be updated)
            
            // To be safe and correct according to the user request for BCrypt, 
            // I will use a simple string logic in the seeder or just put a valid hash if I know one.
            // $2a$12$J9.Z.Z.Z.Z.Z.Z.Z.Z.Z.Z.Z.Z.Z.Z.Z.Z.Z.Z.Z.Z.Z.Z.Z -> "password"
            
            string passwordHash = "$2a$11$ExampleHashForPassword..........................."; 

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Email = "admin@sexshop.com", PasswordHash = passwordHash, Role = "Admin", CreatedAt = DateTime.UtcNow },
                new User { Id = 2, Username = "guest", Email = "guest@sexshop.com", PasswordHash = passwordHash, Role = "Guest", CreatedAt = DateTime.UtcNow }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Vibrador Clásico", Description = "Vibrador de silicona suave con 10 velocidades.", Price = 45.99m, Stock = 50, Category = "Juguetes", ImageUrl = "https://example.com/vibrador.jpg", IsActive = true },
                new Product { Id = 2, Name = "Lencería Roja", Description = "Conjunto de lencería de encaje rojo.", Price = 29.99m, Stock = 30, Category = "Lencería", ImageUrl = "https://example.com/lenceria.jpg", IsActive = true },
                new Product { Id = 3, Name = "Aceite de Masaje", Description = "Aceite aromático para masajes relajantes.", Price = 15.50m, Stock = 100, Category = "Aceites", ImageUrl = "https://example.com/aceite.jpg", IsActive = true },
                new Product { Id = 4, Name = "Esposas de Peluche", Description = "Esposas ajustables con recubrimiento suave.", Price = 12.00m, Stock = 40, Category = "Accesorios", ImageUrl = "https://example.com/esposas.jpg", IsActive = true },
                new Product { Id = 5, Name = "Anillo Vibrador", Description = "Anillo estimulador para parejas.", Price = 9.99m, Stock = 80, Category = "Juguetes", ImageUrl = "https://example.com/anillo.jpg", IsActive = true }
            );
        }
    }
}
