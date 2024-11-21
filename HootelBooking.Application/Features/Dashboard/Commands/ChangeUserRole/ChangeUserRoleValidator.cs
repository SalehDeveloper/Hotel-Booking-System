using FluentValidation;
using HootelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Dashboard.Commands.ChangeUserRole
{
    public class ChangeUserRoleValidator : AbstractValidator<ChangeUserRoleCommand>
    {
        public ChangeUserRoleValidator()
        {
            RuleFor(x => x.RequestDto.Id).NotEmpty().NotNull();
            RuleFor(x => x.RequestDto.Role).NotEmpty().NotNull()
            .Must(BeValidRole).WithMessage("Invalid Role Type");

        }

        private bool BeValidRole(string role)
        {
            return Enum.TryParse(role, true, out enRoles _);
        }
    }
}
