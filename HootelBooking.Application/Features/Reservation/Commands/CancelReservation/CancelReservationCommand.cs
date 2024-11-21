using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Reservation.Commands.CancelReservation
{
    public class CancelReservationCommand:IRequest<Result<string>>
    {
        public int ReservationId { get; set; }  
    }
}
