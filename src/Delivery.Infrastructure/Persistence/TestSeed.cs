using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.HelperEntities;
using Delivery.Domain.Entities.RestaurantEntities;
using Delivery.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Delivery.Infrastructure.Persistence.Seed;

public static class TestSeed
{
    public static void Seed(ModelBuilder builder)
    {
        // === PRIPREMA ID-jeva (GUID-ova) ===
        #region IDs
        var allergenGlutenId = Guid.NewGuid();
        var allergenLactoseId = Guid.NewGuid();
        var addressRestaurant1Id = Guid.NewGuid();
        var addressCustomer1Id = Guid.NewGuid();
        var ownerProfileId = Guid.NewGuid();
        var customerProfileId = Guid.NewGuid();
        var restaurantId = Guid.NewGuid();
        var menuId = Guid.NewGuid();
        var pizzaId = Guid.NewGuid();
        var pastaId = Guid.NewGuid();
        var pizzaOptionsGroupId = Guid.NewGuid();
        var ketchupId = Guid.NewGuid();
        var oreganoId = Guid.NewGuid();

        // ID-jevi Role-ova (moraju se poklapati sa onima iz DbContext-a)
        var customerRoleId = Guid.Parse("5B00155D-77A2-438C-B18F-DC1CC8AF5A43");
        var ownerRoleId = Guid.Parse("FC7E84F2-E37E-46E2-A222-A839D3E1A3BB");

        // ID-jevi za nove testne User-e
        var customerUserId = Guid.NewGuid();
        var ownerUserId = Guid.NewGuid();
        #endregion

        var passwordHasher = new PasswordHasher<User>();

        // === KORISNICI (Users & Roles) - SADA SU OVDE ===
        var customerUser = new User
        {
            Id = customerUserId,
            UserName = "customer1",
            NormalizedUserName = "CUSTOMER1",
            Email = "customer1@example.com",
            NormalizedEmail = "CUSTOMER1@EXAMPLE.COM",
            FirstName = "Peter",
            LastName = "Peterson",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };
        customerUser.PasswordHash = passwordHasher.HashPassword(customerUser, "CustomerPass1!");

        var ownerUser = new User
        {
            Id = ownerUserId,
            UserName = "owner1",
            NormalizedUserName = "OWNER1",
            Email = "owner1@example.com",
            NormalizedEmail = "OWNER1@EXAMPLE.COM",
            FirstName = "Mark",
            LastName = "Markov",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };
        ownerUser.PasswordHash = passwordHasher.HashPassword(ownerUser, "OwnerPass1!");

        builder.Entity<User>().HasData(customerUser, ownerUser);
        builder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid> { RoleId = customerRoleId, UserId = customerUserId },
            new IdentityUserRole<Guid> { RoleId = ownerRoleId, UserId = ownerUserId }
        );

        // === POMOĆNI ENTITETI ===
        builder.Entity<Allergen>().HasData(
            new Allergen { Id = allergenGlutenId, Name = "Gluten", Type = "Cereals" },
            new Allergen { Id = allergenLactoseId, Name = "Lactose", Type = "Dairy" }
        );

        builder.Entity<Address>().HasData(
            new Address { Id = addressRestaurant1Id, StreetAndNumber = "123 Main St", City = "New York", PostalCode = "10001" },
            new Address { Id = addressCustomer1Id, StreetAndNumber = "221B Baker Street", City = "London", PostalCode = "NW1 6XE" }
        );

        // === PROFILI ===
        // Sada će ovi UserId-jevi postojati kada se ovaj kod izvrši
        builder.Entity<Owner>().HasData(new Owner { Id = ownerProfileId, UserId = ownerUserId });
        builder.Entity<Customer>().HasData(new Customer { Id = customerProfileId, UserId = customerUserId });

        // === RESTORAN, MENI, JELA, DODACI ===
        builder.Entity<Restaurant>().HasData(new Restaurant
        {
            Id = restaurantId,
            Name = "The Gilded Spoon",
            Description = "The best grill in town.",
            AddressId = addressRestaurant1Id,
            OwnerId = ownerProfileId
        });

        builder.Entity<Menu>().HasData(new Menu { Id = menuId, Name = "Main Menu", RestaurantId = restaurantId });

        builder.Entity<Dish>().HasData(
            new Dish { Id = pizzaId, Name = "Capricciosa", Description = "A timeless classic", Price = 12.50, Type = "Pizza", MenuId = menuId },
            new Dish { Id = pastaId, Name = "Carbonara", Description = "Cream and bacon", Price = 10.50, Type = "Pasta", MenuId = menuId }
        );

        builder.Entity<DishOptionGroup>().HasData(
            new DishOptionGroup { Id = pizzaOptionsGroupId, Name = "Pizza Toppings", DishId = pizzaId }
        );

        builder.Entity<DishOption>().HasData(
            new DishOption { Id = ketchupId, Name = "Ketchup", Price = 0.50, DishOptionGroupId = pizzaOptionsGroupId },
            new DishOption { Id = oreganoId, Name = "Oregano", Price = 0.30, DishOptionGroupId = pizzaOptionsGroupId }
        );

        // === MANY-TO-MANY VEZE ===
        builder.Entity("AllergenDish").HasData(
            new { AllergensId = allergenGlutenId, DishesId = pizzaId },
            new { AllergensId = allergenLactoseId, DishesId = pizzaId }
        );

        builder.Entity("AllergenCustomer").HasData(
            new { AllergensId = allergenLactoseId, CustomersId = customerProfileId }
        );
    }
}