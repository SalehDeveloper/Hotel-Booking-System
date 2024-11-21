using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Dashboard.Queries.GetLastAddedUsers
{
    public class GetLastAddedUsersValidator:AbstractValidator<GetLastAddedUsersQuery>
    {
        public GetLastAddedUsersValidator()
        {
            RuleFor(x => x.Days).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
