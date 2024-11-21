using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Reservation.Response;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Reservation.Commands.CheckOut
{
    public class CheckOutCommandHandler : IRequestHandler<CheckOutCommand, Result<ReservationResponseDto>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public CheckOutCommandHandler(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<Result<ReservationResponseDto>> Handle(CheckOutCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(request.ReservationId);

            if (reservation == null)
                return new Result<ReservationResponseDto>(404, $"Reservation with Id: {request.ReservationId} Not Found");

            if (reservation.ReservationStatusID == (int)enReservationStatus.COMPLETED)
                return new Result<ReservationResponseDto>(400, "reservation already Completed");

            if (reservation.ReservationStatusID != (int)enReservationStatus.CHECKEDIN)
                return new Result<ReservationResponseDto>(400, "Only ChickedIn reservations can be ChickedOut.");

            var checkedOutReservation = await _reservationRepository.CheckOutAsync(reservation.ReservationStatusID);

            if (checkedOutReservation == null)
                throw new ErrorResponseException(500, "Operation Failed, Try later", "Internal server error");

            var mappedReservation = _mapper.Map<ReservationResponseDto>(checkedOutReservation);
       

            return new Result<ReservationResponseDto>(mappedReservation, 200, "CheckedOut Successfully");
        }
    }
}
