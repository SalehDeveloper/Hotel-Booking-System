using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Room.Response;
using HootelBooking.Application.Dtos.RoomType.Response;
using HootelBooking.Application.Models;
using MediatR;


namespace HootelBooking.Application.Features.Rooms.Commands.DeleteRoom
{
    public class DeleteRoomCommamdHandler : IRequestHandler<DeleteRoomCommand, Result<RoomResponseDto>>
    { 
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public DeleteRoomCommamdHandler(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<Result<RoomResponseDto>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var roomToDelete = await _roomRepository.GetByIdAsync(request.Id);

            // Check if the room exists
            if (roomToDelete == null)
            {
                return new Result<RoomResponseDto>(404, $"RoomType with Id: {request.Id} Not Found");
            }

            // Attempt to delete the room
            var res = await _roomRepository.DeleteAsync(request.Id);
            var mappedRoom = _mapper.Map<RoomResponseDto>(roomToDelete);

            // Check deletion result and return appropriate response
            if (res == 0)
            {
                return new Result<RoomResponseDto>(mappedRoom, 409, "RoomType is already deleted");
            }

            if (res == request.Id)
            {
                return new Result<RoomResponseDto>(mappedRoom, 200, "RoomType deleted successfully");
            }

            // Fallback if unexpected result
            return new Result<RoomResponseDto>(500, "Unexpected error occurred during deletion");
        }
    }
}
