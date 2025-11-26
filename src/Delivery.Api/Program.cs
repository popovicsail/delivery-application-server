using System.Text;
using Delivery.Api.Hubs;
using Delivery.Api.Middleware;
using Delivery.Application;
using Delivery.Application.Interfaces;
using Delivery.Application.Mappings;
using Delivery.Application.Services;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Infrastructure;
using Delivery.Infrastructure.BackgroundServices.CourierStatusUpdater;
using Delivery.Infrastructure.Persistence;
using Delivery.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Delivery.Api;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build())
            .CreateLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();

            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddSignalR();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("https://localhost:5173", "http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]))
                };
            });

            // ✅ Registracija slojeva
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            // ✅ Servisi
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddHttpClient<IAddressValidationService, AddressValidationService>(client =>
            {
                client.DefaultRequestHeaders.Add("User-Agent", "DeliveryApp/1.0");
            });
            builder.Services.AddHttpClient<IDeliveryTimeService, DeliveryTimeService>();


            // ✅ Middleware
            builder.Services.AddTransient<ExceptionHandlingMiddleware>();

            // ✅ AutoMapper profili
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(RestaurantMappings).Assembly);
                cfg.AddMaps(typeof(RatingProfile).Assembly);
            });

            // ✅ Background services
            builder.Services.AddHostedService<CourierStatusUpdater>();
            builder.Services.AddHostedService<OrderAssignmentBackgroundService>();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // ✅ SignalR hub
            app.MapHub<CourierLocationHub>("/hubs/courierLocation");

            app.UseHttpsRedirection();
            app.UseCors("AllowReactApp");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "ERROR: Fatal error");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
