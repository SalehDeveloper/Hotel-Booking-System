using HootelBooking.Application.Dtos.Room.Request;
using HootelBooking.Application.Dtos.Room.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommand:IRequest<Result<RoomResponseDto>>
    {
        public UpdateRoomRequestDto Room { get; set; }
    }
}
