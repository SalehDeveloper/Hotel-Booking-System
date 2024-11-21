using FluentValidation;
using HootelBooking.Application.Behaviours;
using HootelBooking.Application.Dtos.Country.Request;
using HootelBooking.Application.Features.Amenities.Commands.CreateAmenity;
using HootelBooking.Application.Features.Amenities.Commands.UpdateAmenity;
using HootelBooking.Application.Features.Auth.Commands.ChangePassword;
using HootelBooking.Application.Features.Auth.Commands.ConfirmEmail;
using HootelBooking.Application.Features.Auth.Commands.ConfirmTwoFactorCode;
using HootelBooking.Application.Features.Auth.Commands.ForgotPassword;
using HootelBooking.Application.Features.Auth.Commands.Login;
using HootelBooking.Application.Features.Auth.Commands.Register;
using HootelBooking.Application.Features.Auth.Commands.ResetPassword;
using HootelBooking.Application.Features.Countries.Commands.CreateCountry;
using HootelBooking.Application.Features.Countries.Commands.UpdateCountry;
using HootelBooking.Application.Features.Countries.Queries.GetByCode;
using HootelBooking.Application.Features.Countries.Queries.Search;
using HootelBooking.Application.Features.Dashboard.Commands.AddUser;
using HootelBooking.Application.Features.Dashboard.Commands.ChangeUserRole;
using HootelBooking.Application.Features.Dashboard.Queries.GetByEmail;
using HootelBooking.Application.Features.Dashboard.Queries.GetByRole;
using HootelBooking.Application.Features.Dashboard.Queries.GetLastAddedUsers;
using HootelBooking.Application.Features.FeedBacks.Commands.AddFeedback;
using HootelBooking.Application.Features.FeedBacks.Commands.UpdateFeedback;
using HootelBooking.Application.Features.Payment.Commands.ProccessPayment;
using HootelBooking.Application.Features.Reservation.Commands.CreateReservation;
using HootelBooking.Application.Features.Rooms.Commands.CreateRoom;
using HootelBooking.Application.Features.Rooms.Commands.UpdateRoom;
using HootelBooking.Application.Features.Rooms.Queries.GetByPrice;
using HootelBooking.Application.Features.RoomTypes.Command.CreateRoomType;
using HootelBooking.Application.Features.RoomTypes.Command.UpdateRoomType;
using HootelBooking.Application.Features.States.Commands.AddState;
using HootelBooking.Application.Features.States.Commands.UpdateState;
using HootelBooking.Application.Features.States.Queries.Search;

using HootelBooking.Application.Services;
using HootelBooking.Persistence.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HootelBooking.Application
{
    public static  class ApplicationContainer
    { 
        public static IServiceCollection AddApplicationServices (this IServiceCollection services )
        {
             services.AddAutoMapper(Assembly.GetExecutingAssembly());
          
            // Register MediatR
            services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
           
            // Register the pipeline behavior
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            

            // Register validators
            services.AddTransient<IValidator<CreateCountryCommand>, CreateCountryValidator>();
            services.AddTransient<IValidator<GetByCodeQuery>, GetByCodeValidator>();
            services.AddTransient<IValidator<SearchQuery>, SearchValidator>();
            services.AddTransient<IValidator<UpdateCountryCommand>, UpdateCountryValidator>();
            services.AddTransient<IValidator<StateSearchQuery>, StateSearchValidator>();
            services.AddTransient<IValidator<AddStateCommand>, AddStateValidator>();
            services.AddTransient<IValidator<UpdateStateCommand>, UpdateStateValidator>();
            services.AddTransient<IValidator<CreateAmenityCommand>, CreateAmenityValidator>();
            services.AddTransient<IValidator<UpdateAmenityCommand>, UpdateAmenityValidator>();
            services.AddTransient<IValidator<CreateRoomTypeCommand>, CreateRoomTypeValidator>();
            services.AddTransient<IValidator<UpdateRoomTypeCommand>, UpdateRoomTypeValidator>();
            services.AddTransient<IValidator<GetByPriceQuery>, GetByPriceValidator>();
            services.AddTransient<IValidator<CreateRoomCommand>, CreateRoomValidator>();
            services.AddTransient<IValidator<UpdateRoomCommand>, UpdateRoomValidator>();
            services.AddTransient<IValidator<RegisterCommand>, RegisterValidator>();
            services.AddTransient<IValidator<LoginCommand>, LoginCommandValidator>();
            services.AddTransient<IValidator<ResetPasswordCommand>, ResetPasswordValidator>();
            services.AddTransient<IValidator<ForgotPasswordCommand>, ForgotPasswordValidator>();
            services.AddTransient<IValidator<ChangePasswordCommand>, ChangePasswordValidator>();
            services.AddTransient<IValidator<ConfirmEmailCommand>, ConfirmEmailValidator>();
            services.AddTransient<IValidator<ConfirmTwoFactorCodeCommand>, ConfirmTwoFactorCodeValidator>();
            services.AddTransient<IValidator<AddUserCommand>, AddUserValidator>();
            services.AddTransient<IValidator<GetByEmailQuery>, GetByEmailValidator>();
            services.AddTransient<IValidator<AddUserCommand>, AddUserValidator>();
            services.AddTransient<IValidator<ChangeUserRoleCommand>, ChangeUserRoleValidator>();
            services.AddTransient<IValidator<GetByRoleQuery>, GetByRoleValidator>();
            services.AddTransient<IValidator<GetLastAddedUsersQuery>, GetLastAddedUsersValidator>();
            services.AddTransient<IValidator<ProccessPaymentCommand>, ProccessPaymentValidator>();
            services.AddTransient<IValidator<CreateReservationCommand>, CreateReservationValidator>();
            services.AddTransient<IValidator<UpdateFeedbackCommand>, UpdateFeedbackValidator>();
            services.AddTransient<IValidator<AddFeedbackCommand>, AddFeedbackValidator>();
            services.AddScoped<MessageService>();
            services.AddScoped<ImageService>();



            return services;
        }

    }
}
