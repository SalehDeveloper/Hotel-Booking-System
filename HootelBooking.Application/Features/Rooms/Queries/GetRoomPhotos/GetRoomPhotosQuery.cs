using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Rooms.Queries.GetRoomPhotos
{
    public class GetRoomPhotosQuery:IRequest<Result<IEnumerable<string>>>
    {
        public int RoomId { get; set; } 
    }
}
