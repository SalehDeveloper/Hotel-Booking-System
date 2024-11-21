using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Amenities.Commands.CreateAmenity
{
    public class CreateAmenityValidator:AbstractValidator<CreateAmenityCommand>
    {
        public CreateAmenityValidator()
        {
            RuleFor(x => x.Amenity.Name).NotEmpty().NotNull();
            RuleFor(x=> x.Amenity.IsActive).NotEmpty().NotNull();
            RuleFor(x => x.Amenity.Description).MaximumLength(256);
            

        }
    }
}
