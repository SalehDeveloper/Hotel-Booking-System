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

namespace HootelBooking.Application.Features.Reservation.Queries.GetReservationByID
{
    public class GetReservationByIDQueryHandler : IRequestHandler<GetReservationByIDQuery, Result<ReservationResponseDto>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public GetReservationByIDQueryHandler(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<Result<ReservationResponseDto>> Handle(GetReservationByIDQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(request.ReservationID);

            if (reservation == null)
                return new Result<ReservationResponseDto>(404, $"Reservation With ID: {request.ReservationID} Not Found");
        
            
            var mappedReservation = _mapper.Map<ReservationResponseDto>(reservation);

            return new Result<ReservationResponseDto>(mappedReservation, 200, "Retrived Successfully");
        
        }
    }
}
