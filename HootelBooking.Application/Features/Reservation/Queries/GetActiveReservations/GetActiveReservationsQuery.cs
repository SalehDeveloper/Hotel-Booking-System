using HootelBooking.Application.Dtos.Reservation.Response;
using HootelBooking.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Reservation.Queries.GetActiveReservations
{
    public class GetActiveReservationsQuery:IRequest<Result<List<ReservationResponseDto>>>
    {

    }
}
