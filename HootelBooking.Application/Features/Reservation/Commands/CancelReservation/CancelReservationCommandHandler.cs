using HootelBooking.Application.Contracts;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Enums;
using MediatR;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Reservation.Commands.CancelReservation
{
    
    public class CancelReservationCommandHandler : IRequestHandler<CancelReservationCommand, Result<string>>
    {
       
        private readonly IReservationRepository _reservationRepository;

        public CancelReservationCommandHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<Result<string>> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(request.ReservationId);

               if (reservation is  null)
                return new Result<string>(404, $"Reservation with id:{request.ReservationId} Not Found");



               if (reservation.ReservationStatusID == (int)enReservationStatus.CANCELED )
                    return new Result<string>(400, "Reservation already cancelled");
             
                
               if (reservation.ReservationStatusID ==  (int)enReservationStatus.CHECKEDIN)
                    return new Result<string>(400, "Reservation cannot be canceled after check-in.");

            if (reservation.ReservationStatusID == (int)enReservationStatus.COMPLETED)
                return new Result<string>(400, "Reservation cannot be canceled after check-out.");
        
            
            if (reservation.CheckInDate.AddHours(-24) <= DateTime.Now)
                return new Result<string>(400, "Reservation cannot be cancelled within 24 hours of check-in date.");



            var cancellationResult = await _reservationRepository.CancelReservationAsync(reservation.Id);
            if (cancellationResult)
                return new Result<string>($"Reservation With Id:{reservation.Id} Cancelled Successfully", 200, "Your reservation has been canceled.You will receive a refund, with 20 % deducted as a cancellation fee.");



                throw new ErrorResponseException(500, "Operation Failed, Try latter", "Internal server error");


                
            
            
        }
    }
}
