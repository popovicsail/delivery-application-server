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
    public static void Seed(ModelBuilder modelBuilder)
    {
        // --- Priprema ID-jeva ---
        #region IDs
        // Korisnici i Profili
        var userId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var ownerId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Restoran i Adresa
        var addressId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var restaurantId = Guid.Parse("44444444-4444-4444-4444-444444444444");

        // Meni i Jela
        var menuId = Guid.Parse("55555555-5555-5555-5555-555555555555");
        var dishMargheritaId = Guid.Parse("66666666-6666-6666-6666-666666666666");
        var dishCapricciosaId = Guid.Parse("77777777-7777-7777-7777-777777777777");

        // NOVI ENTITETI
        var allergenGlutenId = Guid.NewGuid();
        var allergenLactoseId = Guid.NewGuid();
        var toppingsGroupId = Guid.NewGuid();
        var ketchupId = Guid.NewGuid();
        var cheeseId = Guid.NewGuid();
        #endregion

        // --- Kreiranje Entiteta ---

        // 1. User
        var user = new User
        {
            Id = userId,
            UserName = "owner1",
            NormalizedUserName = "OWNER1",
            Email = "owner1@example.com",
            NormalizedEmail = "OWNER1@EXAMPLE.COM",
            FirstName = "Petar",
            LastName = "Petrović",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        // ISPRAVKA: Heširanje lozinke
        var passwordHasher = new PasswordHasher<User>();
        user.PasswordHash = passwordHasher.HashPassword(user, "OwnerPass1!");
        modelBuilder.Entity<User>().HasData(user);

        // 2. Owner
        var owner = new Owner { Id = ownerId, UserId = userId };
        modelBuilder.Entity<Owner>().HasData(owner);

        // 3. Address
        var address = new Address
        {
            Id = addressId,
            StreetAndNumber = "Knez Mihailova 12",
            City = "Beograd",
            PostalCode = "11000"
        };
        modelBuilder.Entity<Address>().HasData(address);

        // 4. Restaurant
        var restaurant = new Restaurant
        {
            Id = restaurantId,
            Name = "Pizzeria Roma",
            Description = "Autentična italijanska kuhinja sa peći na drva.",
            AddressId = addressId,
            OwnerId = ownerId
        };
        modelBuilder.Entity<Restaurant>().HasData(restaurant);

        // 5. Menu
        var menu = new Menu { Id = menuId, Name = "Pizza Menu", RestaurantId = restaurantId };
        modelBuilder.Entity<Menu>().HasData(menu);

        // 6. Dishes
        var dish1 = new Dish { Id = dishMargheritaId, Name = "Margherita", Description = "Pica sa paradajz sosom, sirom i bosiljkom.", Price = 650, MenuId = menuId, Type = "Italian" };
        var dish2 = new Dish { Id = dishCapricciosaId, Name = "Capricciosa", Description = "Pica sa šunkom, pečurkama i sirom.", Price = 750, MenuId = menuId, Type = "Italian" };
        modelBuilder.Entity<Dish>().HasData(dish1, dish2);

        // 7. Alergeni (NOVO)
        var allergenGluten = new Allergen { Id = allergenGlutenId, Name = "Gluten", Type = "Cereals" };
        var allergenLactose = new Allergen { Id = allergenLactoseId, Name = "Lactose", Type = "Dairy" };
        modelBuilder.Entity<Allergen>().HasData(allergenGluten, allergenLactose);

        // 8. Grupa Dodataka (NOVO)
        var toppingsGroup = new DishOptionGroup { Id = toppingsGroupId, Type="Zavisni", Name = "Extra Toppings", DishId = dishCapricciosaId };
        modelBuilder.Entity<DishOptionGroup>().HasData(toppingsGroup);

        // 9. Dodaci (NOVO)
        var ketchup = new DishOption { Id = ketchupId, Name = "Ketchup", Price = 50, DishOptionGroupId = toppingsGroupId };
        var extraCheese = new DishOption { Id = cheeseId, Name = "Extra Cheese", Price = 120, DishOptionGroupId = toppingsGroupId };
        modelBuilder.Entity<DishOption>().HasData(ketchup, extraCheese);

        // --- Povezivanje (Many-to-Many) ---

        // Povezujemo alergene sa jelima (NOVO)
        modelBuilder.Entity("AllergenDish").HasData(
            new { AllergensId = allergenGlutenId, DishesId = dishMargheritaId },
            new { AllergensId = allergenLactoseId, DishesId = dishMargheritaId },
            new { AllergensId = allergenGlutenId, DishesId = dishCapricciosaId },
            new { AllergensId = allergenLactoseId, DishesId = dishCapricciosaId }
        );
    }
}