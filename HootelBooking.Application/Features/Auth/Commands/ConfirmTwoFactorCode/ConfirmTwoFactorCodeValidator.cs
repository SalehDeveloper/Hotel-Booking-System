using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Auth.Commands.ConfirmTwoFactorCode
{
    public class ConfirmTwoFactorCodeValidator:AbstractValidator<ConfirmTwoFactorCodeCommand>
    {
        public ConfirmTwoFactorCodeValidator()
        {
            RuleFor(x => x.Request.Email).NotEmpty().NotNull();
            RuleFor(x => x.Request.TwoFactorCode ).NotEmpty().NotNull().MaximumLength(8).MinimumLength(8);
        }
    }
}
