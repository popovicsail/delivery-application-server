using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.RestaurantEntities;
using Delivery.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Persistence;

public static class TestSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var passwordHasher = new PasswordHasher<User>();

        // --- ID-jevi Uloga (moraju se poklapati sa onima iz DbContext-a) ---
        var ownerRoleId = Guid.Parse("fc7e84f2-e37e-46e2-a222-a839d3e1a3bb");
        var customerRoleId = Guid.Parse("5b00155d-77a2-438c-b18f-dc1cc8af5a43");

        // --- Testni Korisnici (Users) ---
        var ownerUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var ownerUser = new User { Id = ownerUserId, UserName = "owner1", NormalizedUserName = "OWNER1", Email = "owner1@example.com", NormalizedEmail = "OWNER1@EXAMPLE.COM", FirstName = "Petar", LastName = "Petrovic", EmailConfirmed = true, SecurityStamp = Guid.NewGuid().ToString() };
        ownerUser.PasswordHash = passwordHasher.HashPassword(ownerUser, "OwnerPass1!");

        var customerUserId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var customerUser = new User { Id = customerUserId, UserName = "customer1", NormalizedUserName = "CUSTOMER1", Email = "customer1@example.com", NormalizedEmail = "CUSTOMER1@EXAMPLE.COM", FirstName = "Marko", LastName = "Markovic", EmailConfirmed = true, SecurityStamp = Guid.NewGuid().ToString() };
        customerUser.PasswordHash = passwordHasher.HashPassword(customerUser, "CustomerPass1!");

        var baseWorkSchedId = Guid.NewGuid();

        modelBuilder.Entity<User>().HasData(ownerUser, customerUser);

        // --- Povezivanje Testnih Korisnika sa Ulogama ---
        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid> { RoleId = ownerRoleId, UserId = ownerUserId },
            new IdentityUserRole<Guid> { RoleId = customerRoleId, UserId = customerUserId }
        );

        // --- Testni Profili ---
        var ownerProfileId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var customerProfileId = Guid.Parse("44444444-4444-4444-4444-444444444444");
        modelBuilder.Entity<Owner>().HasData(new Owner { Id = ownerProfileId, UserId = ownerUserId });
        modelBuilder.Entity<Customer>().HasData(new Customer { Id = customerProfileId, UserId = customerUserId });

        // --- Ostali Test Podaci ---
        var addressId = Guid.NewGuid();
        var restaurantId = Guid.NewGuid();
        var menuId = Guid.Parse("55555555-5555-5555-5555-555555555555");
        var pizzaId = Guid.NewGuid();

        var baseWorkSched = new BaseWorkSched
        {
            Id = baseWorkSchedId,
            Saturday = true,
            Sunday = true,
            WorkDayStart = new TimeSpan(10, 0, 0),
            WorkDayEnd = new TimeSpan(22, 0, 0),
            WeekendStart = new TimeSpan(12, 0, 0),
            WeekendEnd = new TimeSpan(21, 30, 0),
            RestaurantId = restaurantId
        };
        modelBuilder.Entity<BaseWorkSched>().HasData(baseWorkSched);

        modelBuilder.Entity<Address>().HasData(new Address { Id = addressId, StreetAndNumber = "Knez Mihailova 12", City = "Beograd", PostalCode = "11000" });
        modelBuilder.Entity<Restaurant>().HasData(new Restaurant { Id = restaurantId, Name = "Pizzeria Roma", Description = "Autentična italijanska kuhinja.", PhoneNumber = "222", Image = "", AddressId = addressId, OwnerId = ownerProfileId, BaseWorkSchedId = baseWorkSchedId });
        modelBuilder.Entity<Menu>().HasData(new Menu { Id = menuId, Name = "Pizza Menu", RestaurantId = restaurantId });
        modelBuilder.Entity<Dish>().HasData(new Dish { Id = pizzaId, Name = "Capricciosa", Description = "Pica sa šunkom i sirom.", Price = 750, MenuId = menuId, Type = "Pizza" });
    }
}