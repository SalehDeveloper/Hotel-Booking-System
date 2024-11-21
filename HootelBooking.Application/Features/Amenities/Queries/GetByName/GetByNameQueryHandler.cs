using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Amenity.Response;
using HootelBooking.Application.Models;
using MediatR;
using System.Runtime.CompilerServices;


namespace HootelBooking.Application.Features.Amenities.Queries.GetByName
{
    public class GetByNameQueryHandler : IRequestHandler<GetByNameQuery, Result<AmenityResponseDto>>
    { 
        private readonly IAmenityRepository _amenityRepository;
        private readonly IMapper _mapper;

        public GetByNameQueryHandler(IAmenityRepository amenityRepository, IMapper mapper)
        {
            _amenityRepository = amenityRepository;
            _mapper = mapper;
        }

        public async Task<Result<AmenityResponseDto>> Handle(GetByNameQuery request, CancellationToken cancellationToken)
        {
            var res = await _amenityRepository.GetByNameAsync(request.Name);

            if (res is not null)
            {
                var mappedResult = _mapper.Map<AmenityResponseDto>(res);

                return new Result<AmenityResponseDto>(mappedResult, 200, "Retrived Successfully");
            }

            return new Result<AmenityResponseDto>(404, $"Amenity With Name {request.Name} Not Found");
        }
    }
}
