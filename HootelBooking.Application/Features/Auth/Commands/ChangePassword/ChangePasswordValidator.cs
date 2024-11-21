using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordValidator:AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.Request.CurrentPassword).NotEmpty().NotNull().MinimumLength(8);

            RuleFor(x=> x.Request.NewPassword).NotEmpty().NotNull().MinimumLength(8).Matches("[A-Z]")
            .WithMessage("Password must contain at least one uppercase letter")
            .Matches("[0-9]")
            .WithMessage("Password must contain at least one special character");
            

            RuleFor(x => x.Request.Email).NotEmpty().NotNull().EmailAddress();


        }
    }
}
