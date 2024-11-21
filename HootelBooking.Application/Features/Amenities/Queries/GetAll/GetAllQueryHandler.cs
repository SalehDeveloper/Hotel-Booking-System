using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Amenity.Response;
using HootelBooking.Application.Models;
using MediatR;


namespace HootelBooking.Application.Features.Amenities.Queries.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, Result<IEnumerable<AmenityResponseDto>>>
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IAmenityRepository amenityRepository, IMapper mapper)
        {
            _amenityRepository = amenityRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<AmenityResponseDto>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var result = await _amenityRepository.ListAllAsync();

            if  (result.Any())
            {
                var mappedResult = _mapper.Map<IEnumerable<AmenityResponseDto>>(result);

                return new Result<IEnumerable<AmenityResponseDto>>(mappedResult, 200, "Retrived Successfully");
            }

            return new Result<IEnumerable<AmenityResponseDto>>(404, "No Amenities Found");
        }
    }
}
