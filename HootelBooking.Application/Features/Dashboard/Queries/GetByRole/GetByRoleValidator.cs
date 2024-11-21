using FluentValidation;
using HootelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Dashboard.Queries.GetByRole
{
    public class GetByRoleValidator : AbstractValidator<GetByRoleQuery>
    {
        public GetByRoleValidator()
        {
            RuleFor(x => x.Role).NotEmpty().NotNull().Must(beValidRole).WithMessage("invalid Role");
        }

        private bool beValidRole(string role)
        {
            return Enum.TryParse(role, true, out enRoles _);
        }
    }
}
