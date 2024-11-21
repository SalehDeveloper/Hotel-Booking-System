using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordValidator:AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(x=> x.Email).NotEmpty().NotNull().EmailAddress();
        }
    }
}
