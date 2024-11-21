using HootelBooking.Application.Dtos.Room.Request;
using HootelBooking.Application.Dtos.Room.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Rooms.Queries.Search
{
    public class RoomSearchQuery:IRequest<Result<IEnumerable<RoomResponseDto>>>
    {

        public decimal? price { get; set; }
        public string? roomType { get; set; }
        public string? viewType { get; set; }
        public string? bedType { get; set; }

        public RoomSearchQuery(decimal? price, string? roomType, string? viewType, string? bedType)
        {
            this.price = price;
            this.roomType = roomType;
            this.viewType = viewType;
            this.bedType = bedType;
        }


    }
}
