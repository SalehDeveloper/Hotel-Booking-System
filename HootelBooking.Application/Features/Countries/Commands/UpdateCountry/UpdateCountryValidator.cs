using FluentValidation;
using HootelBooking.Application.Features.Countries.Commands.CreateCountry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Countries.Commands.UpdateCountry
{
    public class UpdateCountryValidator:AbstractValidator<UpdateCountryCommand>
    {

        public UpdateCountryValidator()
        {
             RuleFor(x => x.Country.Id).NotEmpty().NotNull().GreaterThan(0);
             RuleFor(x=> x.Country.Name).NotEmpty().NotNull();
             RuleFor(x => x.Country.Code).NotEmpty().NotNull().MaximumLength(3);
             RuleFor(x => x.Country.IsActive).NotEmpty().NotNull();
             

        }
    }
}
