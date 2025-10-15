using System;
using Delivery.Api.Contracts.Auth;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.HelperEntities;
using Delivery.Domain.Entities.RestaurantEntities;
using Delivery.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Persistence.Seed;

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
        var ownerUser = new User { Id = ownerUserId, UserName = "owner1", NormalizedUserName = "OWNER1", Email = "owner1@example.com", NormalizedEmail = "OWNER1@EXAMPLE.COM", FirstName = "Petar", LastName = "Petrovic", EmailConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), ProfilePictureBase64 = DefaultAvatar.Base64 };
        ownerUser.PasswordHash = passwordHasher.HashPassword(ownerUser, "OwnerPass1!");

        var customerUserId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var customerUser = new User { Id = customerUserId, UserName = "customer1", NormalizedUserName = "CUSTOMER1", Email = "customer1@example.com", NormalizedEmail = "CUSTOMER1@EXAMPLE.COM", FirstName = "Marko", LastName = "Markovic", EmailConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), ProfilePictureBase64 = DefaultAvatar.Base64 };
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
        var allergenId = Guid.NewGuid();

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

        modelBuilder.Entity<Allergen>().HasData(
            new Allergen { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Kikiriki", Type = "Orašasti plodovi" },
            new Allergen { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Orašasti plodovi", Type = "Orašasti plodovi" },
            new Allergen { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Mleko", Type = "Mlečni proizvodi" },
            new Allergen { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Jaja", Type = "Životinjski proizvodi" },
            new Allergen { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Riba", Type = "Morski plodovi" },
            new Allergen { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), Name = "Školjke", Type = "Morski plodovi" },
            new Allergen { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), Name = "Rakovi", Type = "Morski plodovi" },
            new Allergen { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Name = "Soja", Type = "Mahunarke" },
            new Allergen { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "Pšenica", Type = "Žitarice" },
            new Allergen { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Gluten", Type = "Žitarice" },
            new Allergen { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Name = "Sezam", Type = "Seme" },
            new Allergen { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Name = "Sulfiti", Type = "Dodaci" },
            new Allergen { Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), Name = "Celer", Type = "Povrće" },
            new Allergen { Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), Name = "Senf", Type = "Začini" },
            new Allergen { Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), Name = "Lupina (lupinovo brašno)", Type = "Mahunarke" },
            new Allergen { Id = Guid.Parse("12121212-1212-1212-1212-121212121212"), Name = "Jagode", Type = "Voće" },
            new Allergen { Id = Guid.Parse("13131313-1313-1313-1313-131313131313"), Name = "Banane", Type = "Voće" },
            new Allergen { Id = Guid.Parse("14141414-1414-1414-1414-141414141414"), Name = "Kivi", Type = "Voće" },
            new Allergen { Id = Guid.Parse("15151515-1515-1515-1515-151515151515"), Name = "Breskve", Type = "Voće" },
            new Allergen { Id = Guid.Parse("16161616-1616-1616-1616-161616161616"), Name = "Paradajz", Type = "Povrće" }
        );


    }
}