using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Reservation.Commands.CreateReservation
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationValidator()
        {
            RuleFor(x => x.RequestDto.NumberOfGuests)
              .NotEmpty().WithMessage("Number of guests is required.")
              .InclusiveBetween(1, 4).WithMessage("Number of guests must be between 1 and 4.");

            RuleFor(x => x.RequestDto.CheckInDate)
                .NotEmpty()
                .Must(BeValidCheckInDate)
                .WithMessage("Check-in date must be today or a future date.")
                 .Custom((date, context) =>
                 {
                     // Normalize date
                     if (date != date.Date)
                         context.InstanceToValidate.RequestDto.CheckInDate = date.Date;
                 }); ;

            RuleFor(x => x.RequestDto.CheckOutDate)
                .NotEmpty()
                .Must(BeValidCheckOutDate)
                .WithMessage("Check-out date must be a future date.")
                .GreaterThan(x => x.RequestDto.CheckInDate)
                .WithMessage("Check-out date must be after the check-in date.")
                .Custom((date, context) =>
              {
                  // Normalize date
                  if (date != date.Date)
                      context.InstanceToValidate.RequestDto.CheckOutDate = date.Date;
               });

            RuleFor(x => x)
                .Must(x => BeValidNumberOfNights(x.RequestDto.CheckInDate, x.RequestDto.CheckOutDate))
                .WithMessage("The reservation period must not exceed 30 nights.");


        }

        private bool BeValidCheckInDate(DateTime checkInDate)
        {
            return checkInDate.Date >= DateTime.Now.Date;
        }

        private bool BeValidCheckOutDate(DateTime checkOutDate)
        {
            return checkOutDate.Date > DateTime.Now.Date;
        }

        private bool BeValidNumberOfNights(DateTime checkInDate, DateTime checkOutDate)
        {
            int numberOfNights = (checkOutDate - checkInDate).Days;
            return numberOfNights > 0 && numberOfNights <= 30;
        }

    }
}
