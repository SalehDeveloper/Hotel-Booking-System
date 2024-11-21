using HootelBooking.Application.Dtos.Room.Request;
using HootelBooking.Application.Dtos.Room.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommand:IRequest<Result<RoomResponseDto>>
    {
        public CreateRoomRequestDto Request { get; set; }
    }
}
