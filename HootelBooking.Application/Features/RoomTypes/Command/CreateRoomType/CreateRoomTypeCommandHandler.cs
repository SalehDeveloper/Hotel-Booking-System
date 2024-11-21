using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.RoomType.Response;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;

namespace HootelBooking.Application.Features.RoomTypes.Command.CreateRoomType
{
    public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, Result<RoomTypeResponseDto>>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateRoomTypeCommandHandler(IRoomTypeRepository roomTypeRepository, IUserRepository userRepository, IMapper mapper)
        {
            _roomTypeRepository = roomTypeRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<RoomTypeResponseDto>> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var roomType = await _roomTypeRepository.GetByType(request.RoomType.Type);

            // Check if the room type already exists
            if (roomType != null)
            {
                return new Result<RoomTypeResponseDto>(404, "RoomType already exists");
            }

            // Map request DTO to RoomType entity for adding
            var roomTypeToAdd = _mapper.Map<RoomType>(request.RoomType);
            string note = string.Empty;

            // Handle amenities if provided
            if (request.RoomType.AmenitiesIds != null && request.RoomType.AmenitiesIds.Count > 0)
            {
                // Retrieve amenities by IDs from request
                var amenities = await _roomTypeRepository.GetAmenitiesByIds(request.RoomType.AmenitiesIds);

                if (amenities == null)
                {
                    return new Result<RoomTypeResponseDto>(404, "No amenities found with the provided IDs");
                }

                // Check for invalid amenities
                var amenitiesIdsInDb = amenities.Select(a => a.ID).ToList();
                var invalidAmenityIds = request.RoomType.AmenitiesIds.Except(amenitiesIdsInDb).ToList();

                if (invalidAmenityIds.Any())
                {
                    return new Result<RoomTypeResponseDto>(404, $"Operation Failed. The following Amenity IDs were not found: {string.Join(", ", invalidAmenityIds)}");
                }

                // Separate active and inactive amenities
                var inActiveAmenities = amenities.Where(x => !x.IsActive).ToList();
                var activeAmenities = amenities.Where(x => x.IsActive).ToList();

                if (inActiveAmenities.Any())
                {
                    note = $"The following amenities are inactive and were not added: {string.Join(", ", inActiveAmenities.Select(a => a.Name))}.";
                }

                roomTypeToAdd.Amenities = activeAmenities;
            }

            // Set creator information and add the room type
            roomTypeToAdd.CreatedBy = await _userRepository.GetCurrentUserNameAsync();
            var addedRoomType = await _roomTypeRepository.AddAsync(roomTypeToAdd);

            // Map to response DTO and return success result
            var mappedRoomType = _mapper.Map<RoomTypeResponseDto>(addedRoomType);
            return new Result<RoomTypeResponseDto>(mappedRoomType, 200, $"Added Successfully.\n{note}");
        }
    }
}