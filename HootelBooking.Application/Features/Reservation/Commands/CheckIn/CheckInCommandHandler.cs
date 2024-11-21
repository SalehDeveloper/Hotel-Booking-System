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

namespace HootelBooking.Application.Features.Reservation.Commands.CheckIn
{
    public class CheckInCommandHandler : IRequestHandler<CheckInCommand, Result<ReservationResponseDto>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public CheckInCommandHandler(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<Result<ReservationResponseDto>> Handle(CheckInCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(request.ReservationId);

            if (reservation == null)
                return new Result<ReservationResponseDto>(404, $"Reservation with Id: {request.ReservationId} Not Found");

            if (reservation.ReservationStatusID == (int)enReservationStatus.CHECKEDIN)
                return new Result<ReservationResponseDto>(400, "reservation already checkedIn");

            if (reservation.ReservationStatusID != (int)enReservationStatus.CONFIRMED)
                return new Result<ReservationResponseDto>(400, "Only confirmed reservations can be ChickedIn.");

            if (DateTime.UtcNow.Date != reservation.CheckInDate.Date)
                return new Result<ReservationResponseDto>(400, $"your check-in date is: {reservation.CheckInDate.ToString("yyyy-MM-dd")} ");

            
            var checkedInReservation = await _reservationRepository.CheckInAsync(reservation.ReservationStatusID);

            if (checkedInReservation == null)
                throw new ErrorResponseException(500, "Operation Failed, Try later", "Internal server error");

            var mappedReservation = _mapper.Map<ReservationResponseDto>(checkedInReservation);
       

            return new Result<ReservationResponseDto>(mappedReservation, 200, "CheckedIn Successfully");


           

        }
    }
}
