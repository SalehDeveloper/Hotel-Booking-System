using HootelBooking.Application.Dtos.Room.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Rooms.Queries.GetInActive
{
    public class GetInActiveQuery:IRequest<Result<IEnumerable<RoomResponseDto>>>
    {


    }

}
