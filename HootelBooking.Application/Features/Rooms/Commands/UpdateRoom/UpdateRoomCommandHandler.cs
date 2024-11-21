using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Room.Response;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using MediatR;
using NuGet.Protocol;


namespace HootelBooking.Application.Features.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Result<RoomResponseDto>>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IRoomStatusRepository _roomStatusRepository;
        private readonly IUserRepository _userRepository;   
        private readonly IMapper _mapper;

        public UpdateRoomCommandHandler(IRoomRepository roomRepository, IRoomTypeRepository roomTypeRepository, IRoomStatusRepository roomStatusRepository, IUserRepository userRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
            _roomStatusRepository = roomStatusRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<RoomResponseDto>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {


            var room = await _roomRepository.GetByIdAsync(request.Room.RoomId);

            // Check if the room exists
            if (room == null)
            {
                return new Result<RoomResponseDto>(404, $"Room With Id: {request.Room.RoomId} Not Found");
            }

            // Check if the room number is unique
            var isRoomNumberUnique = await _roomRepository.GetByRoomNumber(request.Room.RoomNumber);
            if (isRoomNumberUnique != null && isRoomNumberUnique.RoomNumber != request.Room.RoomNumber)
            {
                return new Result<RoomResponseDto>(404, "Room Number Already Exists, Try Another One");
            }

            // Check if the room type is valid
            var isRoomTypeValid = await _roomTypeRepository.GetByType(request.Room.RoomType);
            if (isRoomTypeValid == null)
            {
                return new Result<RoomResponseDto>(404, "Invalid Room Type");
            }

            // Update room details
            var roomStatusId = await _roomStatusRepository.GetByStatus(request.Room.RoomStatus);
            room.RoomStatusID = roomStatusId.ID;
            room.RoomTypeID = isRoomTypeValid.Id;
            room.ModifiedBy = await _userRepository.GetCurrentUserNameAsync();
            _mapper.Map(request.Room, room);

            // Attempt to save the updated room
            var res = await _roomRepository.UpdatedAsync(room);
            if (!res)
            {
                throw new ErrorResponseException(500, "Operation Failed", "Internal Server Error");
            }

            // Return success response
            var mappedResult = _mapper.Map<RoomResponseDto>(room);
            return new Result<RoomResponseDto>(mappedResult, 200, "Updated Successfully");


        }
    }
}
