using HootelBooking.Application.Dtos.Reservation.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Reservation.Queries.GetReservationByID
{
    public class GetReservationByIDQuery:IRequest<Result<ReservationResponseDto>>
    {
        public int ReservationID { get; set; }  
    }
}
