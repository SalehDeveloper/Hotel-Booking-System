using FluentValidation;
using HootelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Dashboard.Commands.AddUser
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserValidator()
        {
            RuleFor(x => x.User.Email).EmailAddress().NotEmpty().NotNull().WithMessage("Please Enter A Valid Email Address");
            RuleFor(x => x.User.UserName).NotNull().NotEmpty().WithMessage("User Name Cannot Be Empty");

            RuleFor(x => x.User.Password)
            .NotNull()
            .NotEmpty().
             MinimumLength(8)
            .Matches("[A-Z]").
             WithMessage("Password must contain at least one uppercase letter")
            .Matches("[0-9]")
        .WithMessage("Password must contain at least one special character");



            RuleFor(x => x.User.Country).NotNull().NotEmpty().WithMessage("Country Name Cannot Be Empty");
            RuleFor(x => x.User.State).NotNull().NotEmpty().WithMessage("State Name Cannot Be Empty");
            RuleFor(x => x.User.Role).NotNull().NotEmpty().Must(beValidRole).WithMessage("Inavlid Role");

        }

        private bool beValidRole(string role)
        {
            return Enum.TryParse<enRoles>(role, true, out var _);
        }
    }
}
