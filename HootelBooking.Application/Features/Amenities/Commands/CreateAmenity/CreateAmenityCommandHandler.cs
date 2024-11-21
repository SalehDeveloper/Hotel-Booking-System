using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Amenity.Response;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;


namespace HootelBooking.Application.Features.Amenities.Commands.CreateAmenity
{
    public class CreateAmenityCommandHandler : IRequestHandler<CreateAmenityCommand, Result<AmenityResponseDto>>
    { 
        private readonly IAmenityRepository _AmenityRepository;
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _mapper;

        public CreateAmenityCommandHandler(IAmenityRepository amenityRepository, IUserRepository userRepository, IMapper mapper)
        {
            _AmenityRepository = amenityRepository;
            _UserRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<AmenityResponseDto>> Handle(CreateAmenityCommand request, CancellationToken cancellationToken)
        {
            var amenityToAdd = _mapper.Map<Amenity>(request.Amenity);
            amenityToAdd.CreatedBy = await _UserRepository.GetCurrentUserNameAsync();
            var res = await _AmenityRepository.AddAsync(amenityToAdd);

            if ( res is not null)
            {
                var mappedResult  =_mapper.Map<AmenityResponseDto>(res);
                return new Result<AmenityResponseDto>(mappedResult, 200, "Added Successfully");
            }

            return new Result<AmenityResponseDto>(500, "Failed To Add");
        }
    }
}
