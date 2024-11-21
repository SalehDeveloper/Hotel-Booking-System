using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Reservation.Response;
using HootelBooking.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Reservation.Queries.GetActiveReservations
{
    public class GetActiveReservationsQueryHandler : IRequestHandler<GetActiveReservationsQuery, Result<List<ReservationResponseDto>>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public GetActiveReservationsQueryHandler(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<ReservationResponseDto>>> Handle(GetActiveReservationsQuery request, CancellationToken cancellationToken)
        {
            var activeReservations = await _reservationRepository.GetActiveReservationsAsync();


            if (activeReservations == null || !activeReservations.Any())
                return new Result<List<ReservationResponseDto>>(404, "No Active Reservations Found");

            var mappedResult = _mapper.Map<List<ReservationResponseDto>>(activeReservations);
            return new Result<List<ReservationResponseDto>>(mappedResult, 200, "Retrived Successfully");
        }
    }
}
