using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Reservation.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Reservation.Queries.GetAllReservations
{
    public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationQuery, Result<List<ReservationResponseDto>>> 
    {
         private readonly IReservationRepository _reservationRepository;
         private readonly IMapper    _mapper;

        public GetAllReservationsQueryHandler(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<ReservationResponseDto>>> Handle(GetAllReservationQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetAllReservationsAsync();

            if (reservations is null || !reservations.Any())
                return new Result<List<ReservationResponseDto>>(404, "No Reservations Fond");

            var mappedReservation = _mapper.Map<List<ReservationResponseDto>>(reservations);

            return new Result<List<ReservationResponseDto>>(mappedReservation, 200, "Retrived Successfully"); 

        }
    }
}
