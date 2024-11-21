using HootelBooking.Application.Dtos.Amenity.Request;
using HootelBooking.Application.Dtos.Amenity.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Amenities.Commands.UpdateAmenity
{
    public class UpdateAmenityCommand:IRequest<Result<AmenityResponseDto>>
    {
        public UpdateAmenityRequestDto Amenity { get; set; }
    }
}
