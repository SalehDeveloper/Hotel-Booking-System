using HootelBooking.Application.Contracts;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Rooms.Commands.DeleteRoomPhotos
{
    public class DeleteRoomPhotosCommandHandler : IRequestHandler<DeleteRoomPhotosCommand, Result<IEnumerable<string>>>
    {  
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomPhotoRepository _roomPhotoRepository;

        public DeleteRoomPhotosCommandHandler(IRoomRepository roomRepository, IRoomPhotoRepository roomPhotoRepository)
        {
            _roomRepository = roomRepository;
            _roomPhotoRepository = roomPhotoRepository;
        }

        public async Task<Result<IEnumerable<string>>> Handle(DeleteRoomPhotosCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetByIdAsync(request.RequestDto.RoomId);

            // Check if the room exists
            if (room == null)
            {
                return new Result<IEnumerable<string>>(404, "Room Not Found");
            }

            // Attempt to delete the provided photos
            var res = await _roomPhotoRepository.DeleteAsync(request.RequestDto.PhotoNames);

            // Check if deletion was successful
            if (res)
            {
                return new Result<IEnumerable<string>>(request.RequestDto.PhotoNames, 200, "Deleted Successfully");
            }

            // Handle case where provided photos were missing
            return new Result<IEnumerable<string>>(404, "Missing Provided Photos");

        }
    }
}
