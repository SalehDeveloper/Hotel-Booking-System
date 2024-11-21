using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Amenity.Response;
using HootelBooking.Application.Models;
using MediatR;


namespace HootelBooking.Application.Features.Amenities.Queries.GetActive
{
    public class GetActiveAmenititesQueryHandler : IRequestHandler<GetActiveAmenitiesQuery, Result<IEnumerable<AmenityResponseDto>>>
    {
        private readonly IAmenityRepository _amenityRepository; 
        private readonly IMapper _mapper;

        public GetActiveAmenititesQueryHandler(IAmenityRepository amenityRepository, IMapper mapper)
        {
            _amenityRepository = amenityRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<AmenityResponseDto>>> Handle(GetActiveAmenitiesQuery request, CancellationToken cancellationToken)
        {
            var result = await _amenityRepository.GetActiveAsync();

            if  (result.Any())
            {
                var mappedResult =  _mapper.Map<IEnumerable<AmenityResponseDto>>(result);

                return new Result<IEnumerable<AmenityResponseDto>>(mappedResult, 200, "Retrived Successfully");

            }

            return new Result<IEnumerable<AmenityResponseDto>>(404, "No Active Amenities Found");
        }
    }
}
