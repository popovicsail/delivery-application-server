using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.FeedbackEntities;
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
        var ownerUser = new User { Id = ownerUserId, UserName = "owner1", NormalizedUserName = "OWNER1", Email = "owner1@example.com", NormalizedEmail = "OWNER1@EXAMPLE.COM", FirstName = "Petar", LastName = "Petrovic", EmailConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), ProfilePictureBase64 = DefaultAvatar.Base64 };
        ownerUser.PasswordHash = passwordHasher.HashPassword(ownerUser, "OwnerPass1!");

        var customerUserId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var customerUser = new User { Id = customerUserId, UserName = "customer1", DateOfBirth = DateTime.UtcNow, NormalizedUserName = "CUSTOMER1", Email = "customer1@example.com", NormalizedEmail = "CUSTOMER1@EXAMPLE.COM", FirstName = "Marko", LastName = "Markovic", EmailConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), ProfilePictureBase64 = DefaultAvatar.Base64 };
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
        var menuId = Guid.NewGuid();
        var pizzaId = Guid.NewGuid();
        var voucherId = Guid.NewGuid();

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

        modelBuilder.Entity<Address>().HasData(new Address { Id = addressId, StreetAndNumber = "Knez Mihailova 12", City = "Beograd", PostalCode = "11000" });
        modelBuilder.Entity<Restaurant>().HasData(new Restaurant { Id = restaurantId, Name = "Pizzeria Roma", Description = "Autentična italijanska kuhinja.", PhoneNumber = "222", Image = "", AddressId = addressId, OwnerId = ownerProfileId });
        modelBuilder.Entity<Menu>().HasData(new Menu { Id = menuId, Name = "Pizza Menu", RestaurantId = restaurantId });
        modelBuilder.Entity<Dish>().HasData(new Dish { Id = pizzaId, Name = "Capricciosa", Description = "Pica sa šunkom i sirom.", Price = 750, MenuId = menuId, Type = "Pizza" });
        modelBuilder.Entity<Voucher>().HasData(new Voucher { Id = voucherId, Name = "TestVaučer", DateIssued = DateTime.UtcNow, DiscountAmount = 1200, CustomerId = customerProfileId });
    }
}