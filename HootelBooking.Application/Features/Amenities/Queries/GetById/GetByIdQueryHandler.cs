using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Amenity.Response;
using HootelBooking.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;

namespace HootelBooking.Application.Features.Amenities.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<AmenityResponseDto>>
    { 
        private readonly IAmenityRepository _amenityRepository; 
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(IAmenityRepository amenityRepository, IMapper mapper)
        {
            _amenityRepository = amenityRepository;
            _mapper = mapper;
        }

        public async Task<Result<AmenityResponseDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _amenityRepository.GetByIdAsync(request.Id);

            if (result != null)
            {
                var mappedResult = _mapper.Map<AmenityResponseDto>(result);
                return new Result<AmenityResponseDto>(mappedResult, 200, "Retrived Successfully");
            }
            return new Result<AmenityResponseDto>(404, $"Amenity With Id: {request.Id} Not Found");
        }
    }
}
