using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Dashboard.Queries.GetByEmail
{
    public class GetByEmailValidator : AbstractValidator<GetByEmailQuery>
    {
        public GetByEmailValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
        }
    }
}
