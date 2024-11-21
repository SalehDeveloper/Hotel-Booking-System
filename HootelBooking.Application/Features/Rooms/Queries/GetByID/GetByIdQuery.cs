using HootelBooking.Application.Dtos.Room.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Rooms.Queries.GetByID
{
    public class GetByIdQuery:IRequest<Result<RoomResponseDto>>
    {
        public int Id { get; set; } 
    }
}
