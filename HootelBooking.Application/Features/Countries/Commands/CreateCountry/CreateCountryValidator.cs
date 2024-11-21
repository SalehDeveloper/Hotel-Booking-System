using FluentValidation;
using HootelBooking.Application.Dtos.Country.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Countries.Commands.CreateCountry
{
    public class CreateCountryValidator : AbstractValidator<CreateCountryCommand>
    {
        public CreateCountryValidator()
        {
            RuleFor(x => x.country.Name).NotEmpty().NotNull().WithMessage("Country name is required.");
            RuleFor(x => x.country.Code).NotEmpty().NotNull().MaximumLength(3);
            RuleFor(x => x.country.IsActive).NotNull().NotEmpty();
            // Add other validation rules as needed
        }
    }
}
