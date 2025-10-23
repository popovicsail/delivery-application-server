using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.OrderEntities;
using Delivery.Domain.Entities.RestaurantEntities;
using Delivery.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Dish> Dishes { get; set; }
    public DbSet<DishOption> DishOptions { get; set; }
    public DbSet<DishOptionGroup> DishOptionGroups { get; set; }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Allergen> Allergens { get; set; }
    public DbSet<BaseWorkSched> BaseWorkScheds { get; set; }
    public DbSet<WorkSchedule> WorkSchedules { get; set; }

    public DbSet<Menu> Menus { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }

    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<Courier> Couriers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Worker> Workers { get; set; }

    public DbSet<Voucher> Vouchers { get; set; }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var passwordHasher = new PasswordHasher<User>();

        var adminRoleId = Guid.Parse("2301d884-221a-4e7d-b509-0113dcc043e1");
        builder.Entity<IdentityRole<Guid>>().HasData(
            new IdentityRole<Guid> { Id = adminRoleId, Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
            new IdentityRole<Guid> { Id = Guid.Parse("5b00155d-77a2-438c-b18f-dc1cc8af5a43"), Name = "Customer", NormalizedName = "CUSTOMER" },
            new IdentityRole<Guid> { Id = Guid.Parse("190d206e-0b99-4d0f-b3fa-da6ceea6d8cb"), Name = "Courier", NormalizedName = "COURIER" },
            new IdentityRole<Guid> { Id = Guid.Parse("fc7e84f2-e37e-46e2-a222-a839d3e1a3bb"), Name = "Owner", NormalizedName = "OWNER" },
            new IdentityRole<Guid> { Id = Guid.Parse("f09ece5a-1c11-4792-815b-4ef1bc6c6c20"), Name = "Worker", NormalizedName = "WORKER" }
        );

        var adminUser1 = new User { Id = Guid.Parse("b22698b8-42a2-4115-9631-1c2d1e2ac5f7"), UserName = "admin1", NormalizedUserName = "ADMIN1", Email = "admin1@example.com", NormalizedEmail = "ADMIN1@EXAMPLE.COM", FirstName = "Main", LastName = "Admin", EmailConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), ProfilePictureBase64 = DefaultAvatar.Base64 };
        var adminUser2 = new User { Id = Guid.Parse("bfd2ac09-67d0-4caa-8042-c6241b4f4f7f"), UserName = "admin2", NormalizedUserName = "ADMIN2", Email = "admin2@example.com", NormalizedEmail = "ADMIN2@EXAMPLE.COM", FirstName = "Second", LastName = "Admin", EmailConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), ProfilePictureBase64 = DefaultAvatar.Base64 };
        var adminUser3 = new User { Id = Guid.Parse("1ddc68db-bb87-4cef-bdf8-d369bc1d5334"), UserName = "admin3", NormalizedUserName = "ADMIN3", Email = "admin3@example.com", NormalizedEmail = "ADMIN3@EXAMPLE.COM", FirstName = "Third", LastName = "Admin", EmailConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), ProfilePictureBase64 = DefaultAvatar.Base64 };

        adminUser1.PasswordHash = passwordHasher.HashPassword(adminUser1, "AdminPass1!");
        adminUser2.PasswordHash = passwordHasher.HashPassword(adminUser2, "AdminPass2!");
        adminUser3.PasswordHash = passwordHasher.HashPassword(adminUser3, "AdminPass3!");

        builder.Entity<User>().HasData(adminUser1, adminUser2, adminUser3);

        builder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid> { RoleId = adminRoleId, UserId = adminUser1.Id },
            new IdentityUserRole<Guid> { RoleId = adminRoleId, UserId = adminUser2.Id },
            new IdentityUserRole<Guid> { RoleId = adminRoleId, UserId = adminUser3.Id }
        );

        builder.Entity<WorkSchedule>(entity =>
        {
            entity.Property(e => e.WorkStart).HasColumnType("time");
            entity.Property(e => e.WorkEnd).HasColumnType("time");
            entity.Property(e => e.Date).HasColumnName("date").HasColumnType("text").IsRequired();
        });

        builder.Entity<Customer>().HasOne(p => p.User).WithOne().HasForeignKey<Customer>(p => p.UserId).IsRequired();
        builder.Entity<Courier>().HasOne(p => p.User).WithOne().HasForeignKey<Courier>(p => p.UserId).IsRequired();
        builder.Entity<Owner>().HasOne(p => p.User).WithOne().HasForeignKey<Owner>(p => p.UserId).IsRequired();
        builder.Entity<Worker>().HasOne(p => p.User).WithOne().HasForeignKey<Worker>(p => p.UserId).IsRequired();
        builder.Entity<Administrator>().HasOne(p => p.User).WithOne().HasForeignKey<Administrator>(p => p.UserId).IsRequired();

        builder.Entity<Customer>().HasMany(c => c.Addresses).WithOne().OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Customer>().HasMany(c => c.Allergens).WithMany(a => a.Customers);

        builder.Entity<Owner>().HasMany(o => o.Restaurants).WithOne(r => r.Owner).HasForeignKey(r => r.OwnerId);

        builder.Entity<Restaurant>().HasOne(r => r.Address).WithOne().OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Restaurant>().HasMany(r => r.Workers).WithOne(w => w.Restaurant).HasForeignKey(w => w.RestaurantId);
        builder.Entity<Restaurant>().HasMany(r => r.Menus).WithOne(m => m.Restaurant).HasForeignKey(m => m.RestaurantId);
        builder.Entity<Restaurant>().HasMany(r => r.WorkSchedules).WithOne().OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Restaurant>().HasOne(r => r.BaseWorkSched).WithOne(b => b.Restaurant).HasForeignKey<BaseWorkSched>(b => b.RestaurantId).OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Menu>().HasMany(m => m.Dishes).WithOne(d => d.Menu).HasForeignKey(d => d.MenuId);

        builder.Entity<Dish>().HasMany(d => d.DishOptionGroups).WithOne(g => g.Dish).HasForeignKey(g => g.DishId);
        builder.Entity<Dish>().HasMany(d => d.Allergens).WithMany(a => a.Dishes);

        builder.Entity<Customer>().HasMany(c => c.Vouchers).WithOne().HasForeignKey(v => v.CustomerId);
        builder.Entity<Voucher>().HasIndex(v => v.Code).IsUnique();
        builder.Entity<Order>().HasIndex(o => o.Id).IsUnique();
        builder.Entity<Order>().HasOne(o => o.Customer).WithMany().HasForeignKey(o => o.CustomerId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Order>().HasOne(o => o.Address).WithMany().HasForeignKey(o => o.AddressId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Order>().HasMany(o => o.Items).WithOne(i => i.Order).HasForeignKey(i => i.OrderId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<OrderItem>().HasIndex(o => o.Id).IsUnique();
        builder.Entity<OrderItem>().HasOne(i => i.Order).WithMany(o => o.Items).HasForeignKey(i => i.OrderId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<OrderItem>().HasOne(i => i.Dish).WithMany().HasForeignKey(i => i.DishId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<OrderItem>().Property(i => i.Quantity).IsRequired();
        builder.Entity<OrderItem>().Property(i => i.Price).HasColumnType("decimal(18,2)");
        builder.Entity<Order>().Property(o => o.TotalPrice).HasColumnType("decimal(18,2)");

        TestSeed.Seed(builder);
    }
}