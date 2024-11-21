using HootelBooking.Application.Dtos.RoomType.Request;
using HootelBooking.Application.Dtos.RoomType.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.RoomTypes.Command.UpdateRoomType
{
    public class UpdateRoomTypeCommand:IRequest<Result<RoomTypeResponseDto>>
    {
        public UpdateRoomTypeRequestDto RoomType { get; set; }
    }
}
