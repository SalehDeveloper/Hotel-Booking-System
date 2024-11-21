using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Amenities.Commands.UpdateAmenity
{
    public class UpdateAmenityValidator:AbstractValidator<UpdateAmenityCommand>
    {
        public UpdateAmenityValidator()
        {
            RuleFor(x => x.Amenity.Name).NotEmpty().NotNull();
            RuleFor(x=>x.Amenity.ID).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Amenity.Description).MaximumLength(256);
            RuleFor(x => x.Amenity.IsActive).NotEmpty().NotNull();
        
        }
    }
}
