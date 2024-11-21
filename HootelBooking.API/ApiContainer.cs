using HootelBooking.Application;
using HootelBooking.Domain.Entities;
using HootelBooking.Persistence.Data;
using HootelBooking.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using HootelBooking.Persistence.Models;
using HootelBooking.Application.Models;
using Hangfire;
using HootelBooking.Persistence.Services;

namespace HootelBooking.API
{
    public static class ApiContainer
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Controllers and JSON Serialization
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            // Add Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Configure Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // Add Persistence and Application Services
            services.AddPersistenceServices(configuration.GetConnectionString("DefaultConnection"));
            services.AddApplicationServices();

            // Configure JWT Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]))
                };
            });

            services.Configure<JwtHelper>(configuration.GetSection("JWT"));
            services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
            services.Configure<RoleHelper>(configuration.GetSection("RoleSettings"));
            services.Configure<ImageHelper>(configuration.GetSection("ImageSettings"));
         

            return services;
        }
    }
}

