using HootelBooking.Application.Dtos.Room.Response;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Rooms.Queries.GetByPrice
{
    public class GetByPriceQuery:IRequest<Result<IEnumerable<RoomResponseDto>>>
    {
        public decimal  Price { get; set; }    
    }
}
