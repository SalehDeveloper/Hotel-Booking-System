using HootelBooking.Application.Dtos.Room.Request;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Rooms.Commands.DeleteRoomPhotos
{
    public class DeleteRoomPhotosCommand:IRequest<Result<IEnumerable<string>>>
    {
        public DeleteRoomPhotosRequestDto RequestDto { get; set; }
    }
}
