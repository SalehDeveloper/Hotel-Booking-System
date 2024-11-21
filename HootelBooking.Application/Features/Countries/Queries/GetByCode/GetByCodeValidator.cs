using FluentValidation;
using HootelBooking.Application.Features.Countries.Queries.GetById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Countries.Queries.GetByCode
{
    public class GetByCodeValidator:AbstractValidator<GetByCodeQuery>
    {

        public GetByCodeValidator()
        {
            RuleFor(x=> x.Code)
                .NotEmpty()
                .NotNull()
                .MinimumLength(1)
                .MaximumLength(5);
        }

    }
}
