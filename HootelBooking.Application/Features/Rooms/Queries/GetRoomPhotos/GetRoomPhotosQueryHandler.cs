using HootelBooking.Application.Contracts;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Rooms.Queries.GetRoomPhotos
{
    public class GetRoomPhotosQueryHandler : IRequestHandler<GetRoomPhotosQuery, Result<IEnumerable<string>>>
    {

        private readonly IRoomRepository _roomRepository;

        public GetRoomPhotosQueryHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<Result<IEnumerable<string>>> Handle(GetRoomPhotosQuery request, CancellationToken cancellationToken)
        {
            var  room = await _roomRepository.GetByIdAsync(request.RoomId);

            if (room is   null)
                return new Result<IEnumerable<string>>(404, $"Room With Id {room.RoomId} Not Found");
            
                var photos = await _roomRepository.GetRoomPhotos(room.RoomId);

                if (photos.Any()) 
                return new Result<IEnumerable<string>>(photos, 200, "Retrived Successfully");

                return new Result<IEnumerable<string>>(photos, 200, "No Photos Found");
            
           




        }
    }
}
