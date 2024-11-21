using HootelBooking.Application.Dtos.Reservation.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Reservation.Commands.CheckOut
{
    public class CheckOutCommand:IRequest<Result<ReservationResponseDto>>
    {
        public int ReservationId { get; set; }  
    }
}
