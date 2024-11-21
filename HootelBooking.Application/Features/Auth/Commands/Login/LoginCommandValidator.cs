using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Auth.Commands.Login
{
    public class LoginCommandValidator:AbstractValidator<LoginCommand>  
    {
        public LoginCommandValidator()
        {
          RuleFor(x=> x.Request.Email).NotEmpty().NotNull().EmailAddress();
          RuleFor(x => x.Request.Password).NotEmpty().NotNull().MinimumLength(8);
        }
    }
}
