using HootelBooking.Application.Dtos.Reservation.Request;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Reservation.Queries.IsRoomAvailable
{
    public class IsRoomAvailableQuery:IRequest<Result<string>>
    {
        public int Id { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }


        public IsRoomAvailableQuery(int id, DateTime checkIn, DateTime checkOut)
        {
            Id = id;
            CheckIn = checkIn;
            CheckOut = checkOut;
        }

     



    }
}
