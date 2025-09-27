using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.HelperEntities;
using Delivery.Domain.Entities.RestaurantEntities;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Infrastructure.Persistence.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishOption> DishOptions { get; set; }
        public DbSet<DishOptionGroup> DishOptionGroups { get; set; }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Allergen> Allergens { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Worker> Workers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var passwordHasher = new PasswordHasher<User>();

            Guid adminRoleId = Guid.Parse("2301D884-221A-4E7D-B509-0113DCC043E1");

            builder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid>
            {
                Id = adminRoleId,
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            });

            builder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid> { Id = Guid.Parse("5B00155D-77A2-438C-B18F-DC1CC8AF5A43"), Name = "Customer", NormalizedName = "CUSTOMER" },
                new IdentityRole<Guid> { Id = Guid.Parse("190D206E-0B99-4D0F-B3FA-DA6CEEA6D8CB"), Name = "Courier", NormalizedName = "COURIER" },
                new IdentityRole<Guid> { Id = Guid.Parse("FC7E84F2-E37E-46E2-A222-A839D3E1A3BB"), Name = "Owner", NormalizedName = "OWNER" },
                new IdentityRole<Guid> { Id = Guid.Parse("F09ECE5A-1C11-4792-815B-4EF1BC6C6C20"), Name = "Worker", NormalizedName = "WORKER" }
            );

            Guid adminUserGuid1 = Guid.Parse("B22698B8-42A2-4115-9631-1C2D1E2AC5F7");
            var adminUser1 = new User
            {
                Id = adminUserGuid1,
                UserName = "admin1",
                NormalizedUserName = "ADMIN1",
                Email = "admin@example1.com",
                NormalizedEmail = "ADMIN@EXAMPLE1.COM",
                FirstName = "Glavni",
                LastName = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            adminUser1.PasswordHash = passwordHasher.HashPassword(adminUser1, "AdminPass1!");
            builder.Entity<User>().HasData(adminUser1);
            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid> { RoleId = adminRoleId, UserId = adminUser1.Id });

            Guid adminUserGuid2 = Guid.Parse("BFD2AC09-67D0-4CAA-8042-C6241B4F4F7F");
            var adminUser2 = new User
            {
                Id = adminUserGuid2,
                UserName = "admin2",
                NormalizedUserName = "ADMIN2",
                Email = "admin@example2.com",
                NormalizedEmail = "ADMIN@EXAMPLE2.COM",
                FirstName = "Glavni",
                LastName = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            adminUser2.PasswordHash = passwordHasher.HashPassword(adminUser2, "AdminPass2!");
            builder.Entity<User>().HasData(adminUser2);
            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid> { RoleId = adminRoleId, UserId = adminUser2.Id });

            Guid adminUserGuid3 = Guid.Parse("1DDC68DB-BB87-4CEF-BDF8-D369BC1D5334");
            var adminUser3 = new User
            {
                Id = adminUserGuid3,
                UserName = "admin3",
                NormalizedUserName = "ADMIN3",
                Email = "admin@example3.com",
                NormalizedEmail = "ADMIN@EXAMPLE3.COM",
                FirstName = "Glavni",
                LastName = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            adminUser3.PasswordHash = passwordHasher.HashPassword(adminUser3, "AdminPass3!");
            builder.Entity<User>().HasData(adminUser3);
            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid> { RoleId = adminRoleId, UserId = adminUser3.Id });

            builder.Entity<WorkSchedule>(entity =>
            {
                entity.Property(e => e.WorkStart).HasColumnType("time");
                entity.Property(e => e.WorkEnd).HasColumnType("time");
            });

            builder.Entity<Customer>().HasOne(p => p.User).WithOne().HasForeignKey<Customer>(p => p.UserId).IsRequired();
            builder.Entity<Courier>().HasOne(p => p.User).WithOne().HasForeignKey<Courier>(p => p.UserId).IsRequired();
            builder.Entity<Owner>().HasOne(p => p.User).WithOne().HasForeignKey<Owner>(p => p.UserId).IsRequired();
            builder.Entity<Worker>().HasOne(p => p.User).WithOne().HasForeignKey<Worker>(p => p.UserId).IsRequired();
            builder.Entity<Administrator>().HasOne(p => p.User).WithOne().HasForeignKey<Administrator>(p => p.UserId).IsRequired();

            builder.Entity<Customer>().HasMany(c => c.Addresses).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Customer>().HasMany(c => c.Allergens).WithMany();

            builder.Entity<Owner>().HasMany(o => o.Restaurants).WithOne(r => r.Owner).HasForeignKey(r => r.OwnerId);

            builder.Entity<Restaurant>().HasOne(r => r.Address).WithOne().OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Restaurant>().HasMany(r => r.Workers).WithOne(w => w.Restaurant).HasForeignKey(w => w.RestaurantId);
            builder.Entity<Restaurant>().HasMany(r => r.Menus).WithOne(m => m.Restaurant).HasForeignKey(m => m.RestaurantId);
            builder.Entity<Restaurant>().HasMany(r => r.WorkSchedules).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Menu>().HasMany(m => m.Dishes).WithOne(d => d.Menu).HasForeignKey(d => d.MenuId);

            builder.Entity<Dish>().HasMany(d => d.DishOptionGroups).WithOne(g => g.Dish).HasForeignKey(g => g.DishId);
            builder.Entity<Dish>().HasMany(d => d.Allergens).WithMany();







            TestSeed.Seed(builder);
        }
    }
}