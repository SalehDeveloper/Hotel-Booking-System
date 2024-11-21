using HootelBooking.Application.Dtos.Reservation.Request;
using HootelBooking.Application.Dtos.Reservation.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Reservation.Commands.CreateReservation
{
    public class CreateReservationCommand:IRequest<Result<ReservationResponseDto>>
    {
        public CreateReservationRequestDto RequestDto { get; set; }
    }
}
