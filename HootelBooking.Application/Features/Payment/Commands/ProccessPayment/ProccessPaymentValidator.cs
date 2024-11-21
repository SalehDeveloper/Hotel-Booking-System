using FluentValidation;
using HootelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Payment.Commands.ProccessPayment
{
    public class ProccessPaymentValidator : AbstractValidator<ProccessPaymentCommand>
    {
        public ProccessPaymentValidator()
        {

            RuleFor(x => x.Request.reservationId).NotEmpty().NotNull();

            RuleFor(x => x.Request.Method).NotEmpty().NotNull().Must(BeValidMethod).WithMessage("invalid payment method");

        }

        private bool BeValidMethod(string method)
        {
            return Enum.TryParse<enPaymentType>(method, true, out var _);
        }
    }
}
