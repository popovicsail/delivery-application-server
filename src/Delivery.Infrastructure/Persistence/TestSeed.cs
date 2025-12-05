using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.FeedbackEntities;
using Delivery.Domain.Entities.OfferEntities;
using Delivery.Domain.Entities.OrderEntities;
using Delivery.Domain.Entities.OrderEntities.Enums;
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
        var courierRoleId = Guid.Parse("190d206e-0b99-4d0f-b3fa-da6ceea6d8cb");


        // --- Testni Korisnici (Users) ---
        var ownerUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var ownerUser = new User { Id = ownerUserId, UserName = "owner1", NormalizedUserName = "OWNER1", Email = "owner1@example.com", NormalizedEmail = "OWNER1@EXAMPLE.COM", FirstName = "Petar", LastName = "Petrovic", EmailConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), ProfilePictureBase64 = DefaultAvatar.Base64 };
        ownerUser.PasswordHash = passwordHasher.HashPassword(ownerUser, "OwnerPass1!");


        var customerUserId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var customerUser = new User { Id = customerUserId, UserName = "customer1", DateOfBirth = DateTime.UtcNow, NormalizedUserName = "CUSTOMER1", Email = "customer1@example.com", NormalizedEmail = "CUSTOMER1@EXAMPLE.COM", FirstName = "Marko", LastName = "Markovic", EmailConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), ProfilePictureBase64 = DefaultAvatar.Base64 };
        customerUser.PasswordHash = passwordHasher.HashPassword(customerUser, "CustomerPass1!");

        var courierUserId = Guid.Parse("99999999-9999-9999-9999-999999999999");
        var courierUser = new User
        {
            Id = courierUserId,
            UserName = "courier1",
            NormalizedUserName = "COURIER1",
            Email = "courier1@example.com",
            NormalizedEmail = "COURIER1@EXAMPLE.COM",
            FirstName = "Nikola",
            LastName = "Nikolic",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            ProfilePictureBase64 = DefaultAvatar.Base64
        };
        courierUser.PasswordHash = passwordHasher.HashPassword(courierUser, "CourierPass1!");

        var baseWorkSchedId = Guid.NewGuid();

        modelBuilder.Entity<User>().HasData(ownerUser, customerUser, courierUser);

        // --- Povezivanje Testnih Korisnika sa Ulogama ---
        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid> { RoleId = ownerRoleId, UserId = ownerUserId },
            new IdentityUserRole<Guid> { RoleId = customerRoleId, UserId = customerUserId },
            new IdentityUserRole<Guid> { RoleId = courierRoleId, UserId = courierUserId }
        );

        // --- Testni Profili ---
        var ownerProfileId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var customerProfileId = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var courierProfileId = Guid.Parse("55555555-5555-5555-5555-555555555555");
        modelBuilder.Entity<Owner>().HasData(new Owner { Id = ownerProfileId, UserId = ownerUserId });
        modelBuilder.Entity<Customer>().HasData(new Customer { Id = customerProfileId, UserId = customerUserId });
        modelBuilder.Entity<Courier>().HasData(new Courier { Id = courierProfileId, UserId = courierUserId , WorkStatus = "NEAKTIVAN" });

        // --- Ostali Test Podaci ---
        var addressId = Guid.NewGuid();
        var customerAddressId = Guid.NewGuid();
        var restaurantId = Guid.NewGuid();

        var menuId = Guid.NewGuid();
        var menuId2 = Guid.NewGuid();
        var menuId3 = Guid.NewGuid();

        var pizzaId = Guid.NewGuid();
        var pizzaId2 = Guid.NewGuid();
        var pizzaId3 = Guid.NewGuid();
        var pizzaId4 = Guid.NewGuid();
        var pizzaId5 = Guid.NewGuid();

        var rostilj = Guid.NewGuid();
        var rostilj2 = Guid.NewGuid();
        var rostilj3 = Guid.NewGuid();
        var rostilj4 = Guid.NewGuid();
        var rostilj5 = Guid.NewGuid();

        var dezertId = Guid.NewGuid();
        var dezertId2 = Guid.NewGuid();
        var dezertId3 = Guid.NewGuid();
        var dezertId4 = Guid.NewGuid();
        var dezertId5 = Guid.NewGuid();
        var voucherId = Guid.NewGuid();

        var offerId = Guid.NewGuid();
        var offerId2 = Guid.NewGuid();
        var offerId3 = Guid.NewGuid();
        var offerId4 = Guid.NewGuid();
        var offerId5 = Guid.NewGuid();
        var offerId6 = Guid.NewGuid();

        var dishOptionGroupId = Guid.NewGuid();
        var dishOptionGroupId2 = Guid.NewGuid();
        var dishOptionGroupId3 = Guid.NewGuid();
        var dishOptionGroupId4 = Guid.NewGuid();
        var dishOptionGroupId5 = Guid.NewGuid();
        var dishOptionGroupId6 = Guid.NewGuid();
        var dishOptionGroupId7 = Guid.NewGuid();
        var dishOptionGroupId8 = Guid.NewGuid();
        var dishOptionGroupId9 = Guid.NewGuid();
        var dishOptionGroupId10 = Guid.NewGuid();
        var dishOptionGroupId11 = Guid.NewGuid();
        var dishOptionGroupId12 = Guid.NewGuid();
        var dishOptionGroupId13 = Guid.NewGuid();
        var dishOptionGroupId14 = Guid.NewGuid();
        var dishOptionGroupId15 = Guid.NewGuid();
        var dishOptionGroupId16 = Guid.NewGuid();
        var dishOptionGroupId17 = Guid.NewGuid();
        var dishOptionGroupId18 = Guid.NewGuid();
        var dishOptionGroupId19 = Guid.NewGuid();
        var dishOptionGroupId20 = Guid.NewGuid();
        var dishOptionGroupId21 = Guid.NewGuid();
        var dishOptionGroupId22 = Guid.NewGuid();
        var dishOptionGroupId23 = Guid.NewGuid();
        var dishOptionGroupId24 = Guid.NewGuid();
        var dishOptionGroupId25 = Guid.NewGuid();
        var dishOptionGroupId26 = Guid.NewGuid();
        var dishOptionGroupId27 = Guid.NewGuid();
        var dishOptionGroupId28 = Guid.NewGuid();
        var dishOptionGroupId29 = Guid.NewGuid();
        var dishOptionGroupId30 = Guid.NewGuid();

        modelBuilder.Entity<FeedbackQuestion>().HasData(
            new FeedbackQuestion
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Text = "Kako biste ocenili jednostavnost korišćenja platforme?"
            },
            new FeedbackQuestion
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Text = "Da li ste zadovoljni brzinom rada aplikacije ili sajta?"
            },
            new FeedbackQuestion
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Text = "Koliko ste zadovoljni tačnošću prikazanih informacija o restoranima i jelima?"
            },
            new FeedbackQuestion
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Text = "Kako biste ocenili opšte korisničko iskustvo na našoj platformi?"
            }
        );

        modelBuilder.Entity<FeedbackResponse>().HasData(
        new FeedbackResponse
        {
            Id = Guid.Parse("1a2b3c4d-5e6f-4789-9abc-def012345678"),
            QuestionId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Rating = 5,
            Comment = "Veoma jednostavno za korišćenje!",
            CreatedAt = DateTime.UtcNow,
            UserId = Guid.Parse("11111111-1111-1111-1111-111111111111")
        },
        new FeedbackResponse
        {
            Id = Guid.Parse("2b3c4d5e-6f70-489a-abcd-ef0123456789"),
            QuestionId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Rating = 4,
            Comment = "Brzo se učitava, bez zastajkivanja.",
            CreatedAt = DateTime.UtcNow,
            UserId = Guid.Parse("11111111-1111-1111-1111-111111111111")
        },
        new FeedbackResponse
        {
            Id = Guid.Parse("3c4d5e6f-7081-49ab-bcde-f01234567890"),
            QuestionId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Rating = 3,
            Comment = "Nekad nisu tačne sve informacije o radnom vremenu.",
            CreatedAt = DateTime.UtcNow,
            UserId = Guid.Parse("11111111-1111-1111-1111-111111111111")
        },
        new FeedbackResponse
        {
            Id = Guid.Parse("4d5e6f70-8192-4abc-cdef-012345678901"),
            QuestionId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            Rating = 5,
            Comment = "Generalno sjajno korisničko iskustvo!",
            CreatedAt = DateTime.UtcNow,
            UserId = Guid.Parse("11111111-1111-1111-1111-111111111111")
        },

        new FeedbackResponse
        {
            Id = Guid.Parse("5e6f7081-92a3-4bcd-def0-123456789012"),
            QuestionId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Rating = 4,
            Comment = "Aplikacija je pregledna i laka za korišćenje.",
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            UserId = Guid.Parse("22222222-2222-2222-2222-222222222222")
        },
        new FeedbackResponse
        {
            Id = Guid.Parse("6f708192-a3b4-4cde-ef01-234567890123"),
            QuestionId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Rating = 5,
            Comment = "Radi glatko čak i na sporijem internetu.",
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            UserId = Guid.Parse("22222222-2222-2222-2222-222222222222")
        },
        new FeedbackResponse
        {
            Id = Guid.Parse("708192a3-b4c5-4def-f012-345678901234"),
            QuestionId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Rating = 4,
            Comment = "",
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            UserId = Guid.Parse("22222222-2222-2222-2222-222222222222")
        },
        new FeedbackResponse
        {
            Id = Guid.Parse("8192a3b4-c5d6-4ef0-0123-456789012345"),
            QuestionId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            Rating = 3,
            Comment = "Moglo bi biti intuitivnije u sekciji narudžbina.",
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            UserId = Guid.Parse("22222222-2222-2222-2222-222222222222")
        }
    );

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

        modelBuilder.Entity<Allergen>().HasData(
            new Allergen { Id = Guid.NewGuid(), Name = "Kikiriki", Type = "Orašasti plodovi" },
            new Allergen { Id = Guid.NewGuid(), Name = "Jagode", Type = "Voće" },
            new Allergen { Id = Guid.NewGuid(), Name = "Banane", Type = "Voće" },
            new Allergen { Id = Guid.NewGuid(), Name = "Kivi", Type = "Voće" },
            new Allergen { Id = Guid.NewGuid(), Name = "Breskve", Type = "Voće" },
            new Allergen { Id = Guid.NewGuid(), Name = "Paradajz", Type = "Povrće" },
            new Allergen { Id = Guid.NewGuid(), Name = "Celer", Type = "Povrće" },
            new Allergen { Id = Guid.NewGuid(), Name = "Gluten", Type = "Žitarice" },
            new Allergen { Id = Guid.NewGuid(), Name = "Pšenica", Type = "Žitarice" },
            new Allergen { Id = Guid.NewGuid(), Name = "Školjke", Type = "Morski plodovi" }
            );

        modelBuilder.Entity<Address>().HasData(new Address { Id = addressId, StreetAndNumber = "Bulevar Vojvode Stepe 23", City = "Novi Sad", PostalCode = "21000", Latitude = 45.256395, Longitude = 19.799519 });
        modelBuilder.Entity<Address>().HasData(new Address { Id = customerAddressId, StreetAndNumber = "Bulevar Cara Lazara 44", City = "Novi Sad", PostalCode = "21000", Latitude = 45.248238, Longitude = 19.849496, CustomerId = customerProfileId });
        modelBuilder.Entity<Restaurant>().HasData(new Restaurant { Id = restaurantId, Name = "Pizzeria Roma", Description = "Autentična italijanska kuhinja.", PhoneNumber = "222", Image = "", AddressId = addressId, OwnerId = ownerProfileId });
        modelBuilder.Entity<Menu>().HasData(new Menu { Id = menuId, Name = "Pizza Menu", RestaurantId = restaurantId });
        modelBuilder.Entity<Dish>().HasData(new Dish { Id = pizzaId, Name = "Capricciosa", Description = "Klasična italijanska pizza sa šunkom, pečurkama, maslinama i sirom.", Price = 750, MenuId = menuId, Type = "Pizza" });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId, Name = "Premazi i Zacini", Type = "independent", DishId = pizzaId });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Paradajz Sos", Price = 30, DishOptionGroupId = dishOptionGroupId });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Pesto Sos", Price = 50, DishOptionGroupId = dishOptionGroupId });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Origano", Price = 0, DishOptionGroupId = dishOptionGroupId });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Ljuta Papričica", Price = 0, DishOptionGroupId = dishOptionGroupId });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId2, Name = "Veličina Pizze", Type = "choice", DishId = pizzaId });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Mala (25cm)", Price = -200, DishOptionGroupId = dishOptionGroupId2 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Srednja (30cm)", Price = 0, DishOptionGroupId = dishOptionGroupId2 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Velika (35cm)", Price = 200, DishOptionGroupId = dishOptionGroupId2 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Porcija (40cm)", Price = 400, DishOptionGroupId = dishOptionGroupId2 });


        modelBuilder.Entity<Dish>().HasData(new Dish { Id = pizzaId2, Name = "Quattro Formaggi", Description = "Bogata pizza sa četiri vrste sira: mozzarella, gorgonzola, parmezan i edamer.", Price = 790, MenuId = menuId, Type = "Pizza" });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId3, Name = "Premazi i Zacini", Type = "independent", DishId = pizzaId2 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Paradajz Sos", Price = 30, DishOptionGroupId = dishOptionGroupId3 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Pesto Sos", Price = 50, DishOptionGroupId = dishOptionGroupId3 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Origano", Price = 0, DishOptionGroupId = dishOptionGroupId3 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Ljuta Papričica", Price = 0, DishOptionGroupId = dishOptionGroupId3 });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId4, Name = "Veličina Pizze", Type = "choice", DishId = pizzaId2 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Mala (25cm)", Price = -200, DishOptionGroupId = dishOptionGroupId4 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Srednja (30cm)", Price = 0, DishOptionGroupId = dishOptionGroupId4 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Velika (35cm)", Price = 200, DishOptionGroupId = dishOptionGroupId4 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Porcija (40cm)", Price = 400, DishOptionGroupId = dishOptionGroupId4 });



        modelBuilder.Entity<Dish>().HasData(new Dish { Id = pizzaId3, Name = "Vegetariana", Description = "Lagani miks povrća: paprika, tikvica, pečurke, luk i masline.", Price = 650, MenuId = menuId, Type = "Pizza" });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId5, Name = "Premazi i Zacini", Type = "independent", DishId = pizzaId3 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Paradajz Sos", Price = 30, DishOptionGroupId = dishOptionGroupId5 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Pesto Sos", Price = 50, DishOptionGroupId = dishOptionGroupId5 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Origano", Price = 0, DishOptionGroupId = dishOptionGroupId5 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Ljuta Papričica", Price = 0, DishOptionGroupId = dishOptionGroupId5 });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId6, Name = "Veličina Pizze", Type = "choice", DishId = pizzaId3 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Mala (25cm)", Price = -200, DishOptionGroupId = dishOptionGroupId6 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Srednja (30cm)", Price = 0, DishOptionGroupId = dishOptionGroupId6 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Velika (35cm)", Price = 200, DishOptionGroupId = dishOptionGroupId6 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Porcija (40cm)", Price = 400, DishOptionGroupId = dishOptionGroupId6 });


        modelBuilder.Entity<Dish>().HasData(new Dish { Id = pizzaId4, Name = "Diavola", Description = "Za ljubitelje ljutog — pikantna kobasica, čili sos i jalapeno papričice.", Price = 720, MenuId = menuId, Type = "Pizza" });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId7, Name = "Premazi i Zacini", Type = "independent", DishId = pizzaId4 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Paradajz Sos", Price = 30, DishOptionGroupId = dishOptionGroupId7 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Pesto Sos", Price = 50, DishOptionGroupId = dishOptionGroupId7 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Origano", Price = 0, DishOptionGroupId = dishOptionGroupId7 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Ljuta Papričica", Price = 0, DishOptionGroupId = dishOptionGroupId7 });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId8, Name = "Veličina Pizze", Type = "choice", DishId = pizzaId4 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Mala (25cm)", Price = -200, DishOptionGroupId = dishOptionGroupId8 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Srednja (30cm)", Price = 0, DishOptionGroupId = dishOptionGroupId8 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Velika (35cm)", Price = 200, DishOptionGroupId = dishOptionGroupId8 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Porcija (40cm)", Price = 400, DishOptionGroupId = dishOptionGroupId8 })


            ;
        modelBuilder.Entity<Dish>().HasData(new Dish { Id = pizzaId5, Name = "Funghi e Tartufi", Description = "Aromatična pizza sa miksom šumskih pečuraka i kremastim tartuf sosom.", Price = 880, MenuId = menuId, Type = "Pizza" });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId9, Name = "Premazi i Zacini", Type = "independent", DishId = pizzaId5 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Paradajz Sos", Price = 30, DishOptionGroupId = dishOptionGroupId9 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Pesto Sos", Price = 50, DishOptionGroupId = dishOptionGroupId9 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Origano", Price = 0, DishOptionGroupId = dishOptionGroupId9 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Ljuta Papričica", Price = 0, DishOptionGroupId = dishOptionGroupId9 });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId10, Name = "Veličina Pizze", Type = "choice", DishId = pizzaId5 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Mala (25cm)", Price = -200, DishOptionGroupId = dishOptionGroupId10 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Srednja (30cm)", Price = 0, DishOptionGroupId = dishOptionGroupId10 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Velika (35cm)", Price = 200, DishOptionGroupId = dishOptionGroupId10 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Porcija (40cm)", Price = 400, DishOptionGroupId = dishOptionGroupId10 });



        modelBuilder.Entity<Dish>().HasData(new Dish { Id = rostilj, Name = "Ćevapi", Description = "Tradicionalni balkanski roštilj, posluženi sa lukom i somunom.", Price = 550, MenuId = menuId, Type = "Rostilj" });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId11, Name = "Premazi i Zacini", Type = "independent", DishId = rostilj });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Ajvar", Price = 50, DishOptionGroupId = dishOptionGroupId11 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Luk", Price = 0, DishOptionGroupId = dishOptionGroupId11 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Kajmak", Price = 70, DishOptionGroupId = dishOptionGroupId11 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Pavlaka", Price = 60, DishOptionGroupId = dishOptionGroupId11 });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId12, Name = "Prilozi", Type = "choice", DishId = rostilj });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Pomfrit", Price = 0, DishOptionGroupId = dishOptionGroupId12 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Grilovano Povrće", Price = 100, DishOptionGroupId = dishOptionGroupId12 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Sveža Salata", Price = 80, DishOptionGroupId = dishOptionGroupId12 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Lepinja", Price = 50, DishOptionGroupId = dishOptionGroupId12 });



        modelBuilder.Entity<Dish>().HasData(new Dish { Id = rostilj2, Name = "Pljeskavica", Description = "Sočna pljeskavica od mlevenog mesa, začinjena po domaćinskom receptu.", Price = 600, MenuId = menuId, Type = "Rostilj" });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId13, Name = "Premazi i Zacini", Type = "independent", DishId = rostilj2 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Ajvar", Price = 50, DishOptionGroupId = dishOptionGroupId13 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Luk", Price = 0, DishOptionGroupId = dishOptionGroupId13 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Kajmak", Price = 70, DishOptionGroupId = dishOptionGroupId13 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Pavlaka", Price = 60, DishOptionGroupId = dishOptionGroupId13 });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId14, Name = "Prilozi", Type = "choice", DishId = rostilj2 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Pomfrit", Price = 0, DishOptionGroupId = dishOptionGroupId14 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Grilovano Povrće", Price = 100, DishOptionGroupId = dishOptionGroupId14 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Sveža Salata", Price = 80, DishOptionGroupId = dishOptionGroupId14 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Lepinja", Price = 50, DishOptionGroupId = dishOptionGroupId14 });



        modelBuilder.Entity<Dish>().HasData(new Dish { Id = rostilj3, Name = "Ražnjići", Description = "Mali komadići mesa marinirani i pečeni na roštilju, servirani sa prilogom po izboru.", Price = 650, MenuId = menuId, Type = "Rostilj" });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId15, Name = "Premazi i Zacini", Type = "independent", DishId = rostilj3 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Ajvar", Price = 50, DishOptionGroupId = dishOptionGroupId15 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Luk", Price = 0, DishOptionGroupId = dishOptionGroupId15 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Kajmak", Price = 70, DishOptionGroupId = dishOptionGroupId15 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Pavlaka", Price = 60, DishOptionGroupId = dishOptionGroupId15 });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId16, Name = "Prilozi", Type = "choice", DishId = rostilj3 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Pomfrit", Price = 0, DishOptionGroupId = dishOptionGroupId16 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Grilovano Povrće", Price = 100, DishOptionGroupId = dishOptionGroupId16 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Sveža Salata", Price = 80, DishOptionGroupId = dishOptionGroupId16 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Lepinja", Price = 50, DishOptionGroupId = dishOptionGroupId16 });



        modelBuilder.Entity<Dish>().HasData(new Dish { Id = rostilj4, Name = "Svinjski vrat", Description = "Pečen svinjski vrat na roštilju, uz prilog od povrća.", Price = 700, MenuId = menuId, Type = "Rostilj" });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId17, Name = "Premazi i Zacini", Type = "independent", DishId = rostilj4 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Ajvar", Price = 50, DishOptionGroupId = dishOptionGroupId17 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Luk", Price = 0, DishOptionGroupId = dishOptionGroupId17 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Kajmak", Price = 70, DishOptionGroupId = dishOptionGroupId17 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Pavlaka", Price = 60, DishOptionGroupId = dishOptionGroupId17 });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId18, Name = "Prilozi", Type = "choice", DishId = rostilj4 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Pomfrit", Price = 0, DishOptionGroupId = dishOptionGroupId18 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Grilovano Povrće", Price = 100, DishOptionGroupId = dishOptionGroupId18 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Sveža Salata", Price = 80, DishOptionGroupId = dishOptionGroupId18 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Lepinja", Price = 50, DishOptionGroupId = dishOptionGroupId18 });



        modelBuilder.Entity<Dish>().HasData(new Dish { Id = rostilj5, Name = "Kobasice sa roštilja", Description = "Domaće kobasice pečene na roštilju, servirane sa senfom i hlebom.", Price = 580, MenuId = menuId, Type = "Rostilj" });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId19, Name = "Premazi i Zacini", Type = "independent", DishId = rostilj5 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Ajvar", Price = 50, DishOptionGroupId = dishOptionGroupId19 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Luk", Price = 0, DishOptionGroupId = dishOptionGroupId19 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Kajmak", Price = 70, DishOptionGroupId = dishOptionGroupId19 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Pavlaka", Price = 60, DishOptionGroupId = dishOptionGroupId19 });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId20, Name = "Prilozi", Type = "choice", DishId = rostilj5 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Pomfrit", Price = 0, DishOptionGroupId = dishOptionGroupId20 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Grilovano Povrće", Price = 100, DishOptionGroupId = dishOptionGroupId20 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Sveža Salata", Price = 80, DishOptionGroupId = dishOptionGroupId20 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Lepinja", Price = 50, DishOptionGroupId = dishOptionGroupId20 });



        modelBuilder.Entity<Dish>().HasData(new Dish { Id = dezertId, Name = "Tiramisu", Description = "Klasični italijanski desert sa slojevima piškota, kafe i mascarpone sira.", Price = 400, MenuId = menuId, Type = "Dezert" });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId21, Name = "Dodaci", Type = "independent", DishId = dezertId });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Šlag", Price = 50, DishOptionGroupId = dishOptionGroupId21 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Čokoladni Preliv", Price = 70, DishOptionGroupId = dishOptionGroupId21 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Sveže Voće", Price = 80, DishOptionGroupId = dishOptionGroupId21 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Orašasti Plodovi", Price = 90, DishOptionGroupId = dishOptionGroupId21 });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId22, Name = "Veličina Porcije", Type = "choice", DishId = dezertId });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Mala Porcija", Price = -150, DishOptionGroupId = dishOptionGroupId22 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Standardna Porcija", Price = 0, DishOptionGroupId = dishOptionGroupId22 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Velika Porcija", Price = 150, DishOptionGroupId = dishOptionGroupId22 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Porcija za Deljenje", Price = 300, DishOptionGroupId = dishOptionGroupId22 });



        modelBuilder.Entity<Dish>().HasData(new Dish { Id = dezertId2, Name = "Panna Cotta", Description = "Kremasti desert od kuvanog slatkog mleka, poslužen sa voćnim sosom.", Price = 350, MenuId = menuId, Type = "Dezert" });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId23, Name = "Dodaci", Type = "independent", DishId = dezertId2 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Šlag", Price = 50, DishOptionGroupId = dishOptionGroupId23 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Čokoladni Preliv", Price = 70, DishOptionGroupId = dishOptionGroupId23 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Sveže Voće", Price = 80, DishOptionGroupId = dishOptionGroupId23 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Orašasti Plodovi", Price = 90, DishOptionGroupId = dishOptionGroupId23 });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId24, Name = "Veličina Porcije", Type = "choice", DishId = dezertId2 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Mala Porcija", Price = -150, DishOptionGroupId = dishOptionGroupId24 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Standardna Porcija", Price = 0, DishOptionGroupId = dishOptionGroupId24 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Velika Porcija", Price = 150, DishOptionGroupId = dishOptionGroupId24 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Porcija za Deljenje", Price = 300, DishOptionGroupId = dishOptionGroupId24 });



        modelBuilder.Entity<Dish>().HasData(new Dish { Id = dezertId3, Name = "Čokoladni sufle", Description = "Topli čokoladni sufle sa tečnim čokoladnim centrom, poslužen sa sladoledom od vanile.", Price = 450, MenuId = menuId, Type = "Dezert" });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId25, Name = "Dodaci", Type = "independent", DishId = dezertId3 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Šlag", Price = 50, DishOptionGroupId = dishOptionGroupId25 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Čokoladni Preliv", Price = 70, DishOptionGroupId = dishOptionGroupId25 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Sveže Voće", Price = 80, DishOptionGroupId = dishOptionGroupId25 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Orašasti Plodovi", Price = 90, DishOptionGroupId = dishOptionGroupId25 });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId26, Name = "Veličina Porcije", Type = "choice", DishId = dezertId3 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Mala Porcija", Price = -150, DishOptionGroupId = dishOptionGroupId26 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Standardna Porcija", Price = 0, DishOptionGroupId = dishOptionGroupId26 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Velika Porcija", Price = 150, DishOptionGroupId = dishOptionGroupId26 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Porcija za Deljenje", Price = 300, DishOptionGroupId = dishOptionGroupId26 });



        modelBuilder.Entity<Dish>().HasData(new Dish { Id = dezertId4, Name = "Gelato", Description = "Autentični italijanski sladoled dostupnih ukusa: vanila, čokolada, jagoda i pistaći.", Price = 300, MenuId = menuId, Type = "Dezert" });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId27, Name = "Dodaci", Type = "independent", DishId = dezertId4 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Šlag", Price = 50, DishOptionGroupId = dishOptionGroupId27 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Čokoladni Preliv", Price = 70, DishOptionGroupId = dishOptionGroupId27 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Sveže Voće", Price = 80, DishOptionGroupId = dishOptionGroupId27 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Orašasti Plodovi", Price = 90, DishOptionGroupId = dishOptionGroupId27 });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId28, Name = "Veličina Porcije", Type = "choice", DishId = dezertId4 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Mala Porcija", Price = -150, DishOptionGroupId = dishOptionGroupId28 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Standardna Porcija", Price = 0, DishOptionGroupId = dishOptionGroupId28 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Velika Porcija", Price = 150, DishOptionGroupId = dishOptionGroupId28 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Porcija za Deljenje", Price = 300, DishOptionGroupId = dishOptionGroupId28 });



        modelBuilder.Entity<Dish>().HasData(new Dish { Id = dezertId5, Name = "Cannoli", Description = "Hrskave pržene rolnice punjene slatkim sirom ricotta i čokoladnim komadićima.", Price = 380, MenuId = menuId, Type = "Dezert" });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId29, Name = "Dodaci", Type = "independent", DishId = dezertId5 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Šlag", Price = 50, DishOptionGroupId = dishOptionGroupId29 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Čokoladni Preliv", Price = 70, DishOptionGroupId = dishOptionGroupId29 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Sveže Voće", Price = 80, DishOptionGroupId = dishOptionGroupId29 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Orašasti Plodovi", Price = 90, DishOptionGroupId = dishOptionGroupId29 });
        modelBuilder.Entity<DishOptionGroup>().HasData(new DishOptionGroup { Id = dishOptionGroupId30, Name = "Veličina Porcije", Type = "choice", DishId = dezertId5 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Mala Porcija", Price = -150, DishOptionGroupId = dishOptionGroupId30 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Standardna Porcija", Price = 0, DishOptionGroupId = dishOptionGroupId30 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Velika Porcija", Price = 150, DishOptionGroupId = dishOptionGroupId30 });
        modelBuilder.Entity<DishOption>().HasData(new DishOption { Id = Guid.NewGuid(), Name = "Porcija za Deljenje", Price = 300, DishOptionGroupId = dishOptionGroupId30 });



        modelBuilder.Entity<Offer>().HasData(new Offer { Id = offerId, Name = "Porodična Ponuda", Price = 1250, FreeDelivery = true, ExpiresAt = DateTime.UtcNow.AddMonths(3), MenuId = menuId });
        modelBuilder.Entity<OfferDish>().HasData(new OfferDish { Id = Guid.NewGuid(), OfferId = offerId, DishId = pizzaId, Quantity = 2 });
        modelBuilder.Entity<OfferDish>().HasData(new OfferDish { Id = Guid.NewGuid(), OfferId = offerId, DishId = pizzaId2, Quantity = 1 });



        modelBuilder.Entity<Offer>().HasData(new Offer { Id = offerId2, Name = "Rostilj Za Dvoje", Price = 1000, FreeDelivery = true, ExpiresAt = DateTime.UtcNow.AddMonths(3), MenuId = menuId });
        modelBuilder.Entity<OfferDish>().HasData(new OfferDish { Id = Guid.NewGuid(), OfferId = offerId2, DishId = rostilj, Quantity = 1 });
        modelBuilder.Entity<OfferDish>().HasData(new OfferDish { Id = Guid.NewGuid(), OfferId = offerId2, DishId = rostilj2, Quantity = 1 });



        modelBuilder.Entity<Offer>().HasData(new Offer { Id = offerId3, Name = "Slatki Užitak", Price = 600, FreeDelivery = false, ExpiresAt = DateTime.UtcNow.AddMonths(3), MenuId = menuId });
        modelBuilder.Entity<OfferDish>().HasData(new OfferDish { Id = Guid.NewGuid(), OfferId = offerId3, DishId = dezertId, Quantity = 1 });
        modelBuilder.Entity<OfferDish>().HasData(new OfferDish { Id = Guid.NewGuid(), OfferId = offerId3, DishId = dezertId2, Quantity = 1 });



        modelBuilder.Entity<Offer>().HasData(new Offer { Id = offerId4, Name = "Porodični Rostilj", Price = 2200, FreeDelivery = true, ExpiresAt = DateTime.UtcNow.AddMonths(3), MenuId = menuId });
        modelBuilder.Entity<OfferDish>().HasData(new OfferDish { Id = Guid.NewGuid(), OfferId = offerId4, DishId = rostilj, Quantity = 2 });
        modelBuilder.Entity<OfferDish>().HasData(new OfferDish { Id = Guid.NewGuid(), OfferId = offerId4, DishId = rostilj2, Quantity = 2 });
        modelBuilder.Entity<OfferDish>().HasData(new OfferDish { Id = Guid.NewGuid(), OfferId = offerId4, DishId = rostilj3, Quantity = 1 });



        modelBuilder.Entity<Offer>().HasData(new Offer { Id = offerId5, Name = "Pizza Party", Price = 3000, FreeDelivery = true, ExpiresAt = DateTime.UtcNow.AddMonths(3), MenuId = menuId });
        modelBuilder.Entity<OfferDish>().HasData(new OfferDish { Id = Guid.NewGuid(), OfferId = offerId5, DishId = pizzaId, Quantity = 2 });
        modelBuilder.Entity<OfferDish>().HasData(new OfferDish { Id = Guid.NewGuid(), OfferId = offerId5, DishId = pizzaId3, Quantity = 2 });
        modelBuilder.Entity<OfferDish>().HasData(new OfferDish { Id = Guid.NewGuid(), OfferId = offerId5, DishId = pizzaId4, Quantity = 1 });



        modelBuilder.Entity<Offer>().HasData(new Offer { Id = offerId6, Name = "Dezertni Mix", Price = 900, FreeDelivery = false, ExpiresAt = DateTime.UtcNow.AddMonths(3), MenuId = menuId });
        modelBuilder.Entity<OfferDish>().HasData(new OfferDish { Id = Guid.NewGuid(), OfferId = offerId6, DishId = dezertId3, Quantity = 1 });
        modelBuilder.Entity<OfferDish>().HasData(new OfferDish { Id = Guid.NewGuid(), OfferId = offerId6, DishId = dezertId4, Quantity = 1 });
        modelBuilder.Entity<OfferDish>().HasData(new OfferDish { Id = Guid.NewGuid(), OfferId = offerId6, DishId = dezertId5, Quantity = 1 });



        modelBuilder.Entity<Voucher>().HasData(new Voucher { Id = voucherId, Name = "TestVaučer", DateIssued = DateTime.UtcNow, DiscountAmount = 1200, CustomerId = customerProfileId });



        // --- Lista svih jela (pizza, roštilj, dezerti) ---
        var allDishIds = new List<(Guid Id, string Name, double Price)>
        {
            (pizzaId, "Capricciosa", 750),
            (pizzaId2, "Quattro Formaggi", 790),
            (pizzaId3, "Vegetariana", 650),
            (pizzaId4, "Diavola", 720),
            (pizzaId5, "Funghi e Tartufi", 880),
            (rostilj, "Ćevapi", 550),
            (rostilj2, "Pljeskavica", 600),
            (rostilj3, "Ražnjići", 650),
            (rostilj4, "Svinjski vrat", 700),
            (rostilj5, "Kobasice sa roštilja", 580),
            (dezertId, "Čokoladni kolač", 300),
            (dezertId2, "Palačinke", 350),
            (dezertId3, "Tiramisu", 400),
            (dezertId4, "Voćna salata", 280),
            (dezertId5, "Cheesecake", 420)
        };

        // --- Komentari za restoran po oceni ---
        var restaurantCommentsByScore = new Dictionary<int, List<string>>
        {
            { 1, new List<string> { "Hrana je bila hladna.", "Porcija mala i neukusna.", "Nije vredelo čekanja." } },
            { 2, new List<string> { "Moglo bi biti bolje.", "Nije baš kako sam očekivao.", "Ukus slab, ali jestivo." } },
            { 3, new List<string> { "Solidno, ništa posebno.", "Ok iskustvo, ali ima prostora za poboljšanje.", "Hrana prosečna." } },
            { 4, new List<string> { "Brza priprema i ukusno.", "Restoran je dobar, preporuka.", "Hrana kvalitetna i lepo upakovana." } },
            { 5, new List<string> { "Sve je bilo savršeno.", "Restoran je top!", "Najbolja hrana do sada!" } }
        };

        // --- Komentari za kurira po oceni ---
        var courierCommentsByScore = new Dictionary<int, List<string>>
        {
            { 1, new List<string> { "Kurir je kasnio.", "Kurir nije bio ljubazan.", "Loše iskustvo sa dostavom." } },
            { 2, new List<string> { "Dostava spora.", "Pakovanje loše.", "Kurir nepažljiv." } },
            { 3, new List<string> { "Dostava ok, ništa posebno.", "Kurir neutralan.", "Stiglo na vreme, ali bez utiska." } },
            { 4, new List<string> { "Sve stiglo na vreme.", "Kurir dobar i korektan.", "Pakovanje uredno." } },
            { 5, new List<string> { "Kurir odličan!", "Pakovanje uredno i brzo dostavljeno.", "Najbolja dostava do sada!" } }
        };

        // --- Helper metoda ---
        void SeedOrdersAndRatings(ModelBuilder modelBuilder,
         Guid customerProfileId, Guid customerUserId,
         Guid courierProfileId, Guid restaurantId,
         List<(Guid Id, string Name, double Price)> allDishIds,
         Dictionary<int, List<string>> restaurantCommentsByScore,
         Dictionary<int, List<string>> courierCommentsByScore)
        {
            var rand = new Random();
            var today = DateTime.UtcNow.Date;

            for (int dayOffset = 1; dayOffset <= 364; dayOffset++)
            {
                var date = today.AddDays(-dayOffset);
                int ordersCount = rand.Next(1, 8); // 1–7 dostava po danu

                for (int i = 0; i < ordersCount; i++)
                {
                    var orderId = Guid.NewGuid();
                    var orderItemId = Guid.NewGuid();

                    var createdAt = date.AddHours(rand.Next(10, 22)); // random vreme u toku dana

                    int timeToPrepare = rand.Next(5, 26); // 5–25 min
                    int deliveryTime = rand.Next(10, 26); // 10–25 min

                    var estimatedReadyAt = createdAt.AddMinutes(timeToPrepare);
                    var estimatedDeliveryAt = estimatedReadyAt.AddMinutes(deliveryTime);

                    var dish = allDishIds[rand.Next(allDishIds.Count)];

                    // --- 10% šansa da bude otkazana ---
                    bool isRejected = rand.NextDouble() < 0.10;

                    modelBuilder.Entity<Order>().HasData(new Order
                    {
                        Id = orderId,
                        CustomerId = customerProfileId,
                        CourierId = courierProfileId,
                        RestaurantId = restaurantId,
                        AddressId = customerAddressId,
                        CreatedAt = createdAt,
                        TimeToPrepare = timeToPrepare,
                        EstimatedReadyAt = estimatedReadyAt,
                        DeliveryTimeMinutes = deliveryTime,
                        EstimatedDeliveryAt = estimatedDeliveryAt,
                        Status = isRejected ? OrderStatus.Odbijena.ToString() : OrderStatus.Zavrsena.ToString(),
                        TotalPrice = dish.Price
                    });

                    modelBuilder.Entity<OrderItem>().HasData(new OrderItem
                    {
                        Id = orderItemId,
                        OrderId = orderId,
                        DishId = dish.Id,
                        Name = dish.Name,
                        Quantity = 1,
                        DishPrice = dish.Price,
                        OptionsPrice = 0
                    });

                    // --- Ako nije odbijena, dodaj ocene ---
                    if (!isRejected)
                    {
                        int restaurantScore = rand.Next(1, 6);
                        int courierScore = rand.Next(1, 6);

                        modelBuilder.Entity<Rating>().HasData(
                            new Rating
                            {
                                Id = Guid.NewGuid(),
                                OrderId = orderId,
                                UserId = customerUserId,
                                TargetId = restaurantId,
                                TargetType = RatingTargetType.Restaurant,
                                Score = restaurantScore,
                                Comment = restaurantCommentsByScore[restaurantScore][rand.Next(restaurantCommentsByScore[restaurantScore].Count)],
                                CreatedAt = estimatedDeliveryAt
                            },
                            new Rating
                            {
                                Id = Guid.NewGuid(),
                                OrderId = orderId,
                                UserId = customerUserId,
                                TargetId = courierProfileId,
                                TargetType = RatingTargetType.Courier,
                                Score = courierScore,
                                Comment = courierCommentsByScore[courierScore][rand.Next(courierCommentsByScore[courierScore].Count)],
                                CreatedAt = estimatedDeliveryAt
                            }
                        );
                    }
                }
            }
        }


        // --- Poziv helper metode ---
        SeedOrdersAndRatings(modelBuilder, customerProfileId, customerUserId, courierProfileId, restaurantId, allDishIds, restaurantCommentsByScore, courierCommentsByScore);

    }
}