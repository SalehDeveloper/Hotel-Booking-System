using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.RoomType.Response;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.RoomTypes.Command.UpdateRoomType
{
    internal class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand, Result<RoomTypeResponseDto>>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateRoomTypeCommandHandler(IRoomTypeRepository roomTypeRepository, IUserRepository userRepository, IMapper mapper)
        {
            _roomTypeRepository = roomTypeRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<RoomTypeResponseDto>> Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
        {

            string note = string.Empty;

            var roomType = await _roomTypeRepository.GetByType(request.RoomType.Type);

       
            if (roomType is null)
            {
                return new Result<RoomTypeResponseDto>(404, $"RoomType With Id: {request.RoomType.Id} Not Found");
            }

            // Handling amenities if provided in the request
            if (request.RoomType.AmenitiesIds.Any())
            {
                var amenities = await _roomTypeRepository.GetAmenitiesByIds(request.RoomType.AmenitiesIds);

                // If no amenities are found, return error
                if (amenities is null)
                {
                    return new Result<RoomTypeResponseDto>(404, "No amenities found with the provided IDs.");
                }

                var availableAmenitiesIds = amenities.Select(x => x.ID).ToList();
                var invalidAmenitiesIds = request.RoomType.AmenitiesIds.Except(availableAmenitiesIds);

           
                if (invalidAmenitiesIds.Any())
                {
                    return new Result<RoomTypeResponseDto>(404, $"Operation Failed, Invalid Amenity IDs: ({string.Join(",", invalidAmenitiesIds)})");
                }

                // Handling inactive amenities
                var inactiveAmenities = amenities.Where(a => !a.IsActive).ToList();
                if (inactiveAmenities.Any())
                {
                    note = $"The following amenities are inactive and were not added: ({string.Join(", ", inactiveAmenities.Select(a => a.Name))}).";
                }

                // Set only active amenities
                roomType.Amenities = amenities.Where(a => a.IsActive).ToList();
            }

            // Map the updated RoomType data
            _mapper.Map(request.RoomType, roomType);

            // Set the current user as the modifier
            roomType.ModifiedBy = await _userRepository.GetCurrentUserNameAsync();

          
            var updateResult = await _roomTypeRepository.UpdatedAsync(roomType);

          
            if (!updateResult)
            {
                throw new ErrorResponseException(500, "Operation Failed", "Internal Server Error");
            }

          
            var mappedResult = _mapper.Map<RoomTypeResponseDto>(roomType);
            return new Result<RoomTypeResponseDto>(mappedResult, 200, $"Updated Successfully\n{note}");
        }
    }
}
