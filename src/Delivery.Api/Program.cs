using System.Text;
using Delivery.Api.Middleware;
using Delivery.Application;
using Delivery.Application.Interfaces;
using Delivery.Application.Mappings;
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

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("https://localhost:5173", "http://localhost:5174")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]))
                };
            });

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddTransient<ExceptionHandlingMiddleware>();

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(RestaurantMappings).Assembly);
            });

            builder.Services.AddHostedService<CourierStatusUpdater>();
            builder.Services.AddHostedService<OrderAssignmentBackgroundService>();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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