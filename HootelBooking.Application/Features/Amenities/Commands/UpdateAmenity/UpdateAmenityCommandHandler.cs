using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Amenity.Response;
using HootelBooking.Application.Dtos.State.Response;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using MediatR;


namespace HootelBooking.Application.Features.Amenities.Commands.UpdateAmenity
{
    public class UpdateAmenityCommandHandler : IRequestHandler<UpdateAmenityCommand, Result<AmenityResponseDto>>
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateAmenityCommandHandler(IAmenityRepository amenityRepository, IUserRepository userRepository, IMapper mapper)
        {
            _amenityRepository = amenityRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<AmenityResponseDto>> Handle(UpdateAmenityCommand request, CancellationToken cancellationToken)
        {
            var amenityToUpdate = await _amenityRepository.GetByIdAsync(request.Amenity.ID);

            if (amenityToUpdate == null)
            {
                return new Result<AmenityResponseDto>(404, $"Amenity with Id: {request.Amenity.ID} Not Found");
            }

            _mapper.Map(request.Amenity, amenityToUpdate);
            amenityToUpdate.ModifiedBy = await _userRepository.GetCurrentUserNameAsync();

            var isUpdated = await _amenityRepository.UpdatedAsync(amenityToUpdate);

            if (!isUpdated)
            {
                throw new ErrorResponseException(500, "Operation Failed", "Internal Server Error");
            }

            var mappedResult = _mapper.Map<AmenityResponseDto>(amenityToUpdate);

            return new Result<AmenityResponseDto>(mappedResult, 200, "Updated Successfully");
        }
    }
}
