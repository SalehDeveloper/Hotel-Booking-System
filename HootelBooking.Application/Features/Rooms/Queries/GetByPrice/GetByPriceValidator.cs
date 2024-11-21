using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Rooms.Queries.GetByPrice
{
    public class GetByPriceValidator:AbstractValidator<GetByPriceQuery>
    {
        public GetByPriceValidator()
        {

            RuleFor(x => x.Price).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
