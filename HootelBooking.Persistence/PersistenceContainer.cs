using HootelBooking.Application.Contracts;
using HootelBooking.Application.Services;
using HootelBooking.Persistence.Data;
using HootelBooking.Persistence.Repositories;
using HootelBooking.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;



namespace HootelBooking.Persistence
{
    public static class PersistenceContainer
    { 

        public static IServiceCollection AddPersistenceServices( this IServiceCollection services , string connection)
        {
            services.AddDbContext<AppDbContext>(cfg => cfg.UseSqlServer(connection));

            // add other services 
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IAmenityRepository, AmenityRepository>();
            services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IRoomStatusRepository, RoomStatusRepository>();
            services.AddScoped<IReservationStatusRepository, ReservationStatusRepository>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddTransient<IEmailService , EmailService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAuthorizationRepository, AuthorizationRepository>();
            services.AddTransient<IRoomPhotoRepository, RoomPhotoRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IFeedBackRepository, FeedBackRepository>();



            return services;
        }
    }
}
