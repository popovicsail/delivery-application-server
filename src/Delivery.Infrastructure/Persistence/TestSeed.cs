using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.HelperEntities;
using Delivery.Domain.Entities.RestaurantEntities;
using Delivery.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Persistence.Seed
{
    public static class TestSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // 1. User
            var userId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var user = new User
            {
                Id = userId,
                UserName = "owner1",
                NormalizedUserName = "OWNER1",
                Email = "owner1@example.com",
                NormalizedEmail = "OWNER1@EXAMPLE.COM",
                FirstName = "Petar",
                LastName = "Petrović",
                ProfilePictureUrl = null,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = "" // možeš ostaviti prazno za test
            };
            modelBuilder.Entity<User>().HasData(user);

            // 2. Owner
            var ownerId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var owner = new Owner
            {
                Id = ownerId,
                UserId = userId
            };
            modelBuilder.Entity<Owner>().HasData(owner);

            // 3. Address
            var addressId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var address = new Address
            {
                Id = addressId,
                StreetAndNumber = "Knez Mihailova 12",
                City = "Beograd",
                PostalCode = "11000"
            };
            modelBuilder.Entity<Address>().HasData(address);

            // 4. Restaurant
            var restaurantId = Guid.Parse("44444444-4444-4444-4444-444444444444");
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
            var menuId = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var menu = new Menu
            {
                Id = menuId,
                Name = "Pizza Menu",
                RestaurantId = restaurantId
            };
            modelBuilder.Entity<Menu>().HasData(menu);

            // 6. Dishes
            var dish1Id = Guid.Parse("66666666-6666-6666-6666-666666666666");
            var dish2Id = Guid.Parse("77777777-7777-7777-7777-777777777777");

            var dish1 = new Dish
            {
                Id = dish1Id,
                Name = "Margherita",
                Description = "Pica sa paradajz sosom, sirom i bosiljkom.",
                Price = 650,
                MenuId = menuId,
                Type = "Italian"
            };
            var dish2 = new Dish
            {
                Id = dish2Id,
                Name = "Capricciosa",
                Description = "Pica sa šunkom, pečurkama i sirom.",
                Price = 750,
                MenuId = menuId,
                Type = "Italian"
            };
            modelBuilder.Entity<Dish>().HasData(dish1, dish2);
        }
    }
}
