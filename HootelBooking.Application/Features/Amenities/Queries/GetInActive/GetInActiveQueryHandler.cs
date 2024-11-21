using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Amenity.Response;
using HootelBooking.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Amenities.Queries.GetInActive
{
    public class GetInActiveQueryHandler : IRequestHandler<GetInActiveQuery, Result<IEnumerable<AmenityResponseDto>>>
    { 
         
        private readonly IAmenityRepository _amenityRepository;
        private readonly IMapper _mapper;

        public GetInActiveQueryHandler(IAmenityRepository amenityRepository, IMapper mapper)
        {
            _amenityRepository = amenityRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<AmenityResponseDto>>> Handle(GetInActiveQuery request, CancellationToken cancellationToken)
        {
            var result = await _amenityRepository.GetInActiveAsync();

            if (result.Any())
            {
                var mappedResult = _mapper.Map<IEnumerable<AmenityResponseDto>>(result);

                return new Result<IEnumerable<AmenityResponseDto>>(mappedResult, 200, "Retrived Successfully");
            }
            return new Result<IEnumerable<AmenityResponseDto>>(404, "No InActive Amenities Found");
        }
    }
}
