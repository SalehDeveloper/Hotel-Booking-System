using HootelBooking.Application.Dtos.Room.Request;
using HootelBooking.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Rooms.Commands.AddRoomPhotos
{
    public class AddRoomPhotosCommand:IRequest<Result<IEnumerable<string>>>
    {
       public AddRoomPhotosRequestDto RequestDto { get; set; }
    }
}
