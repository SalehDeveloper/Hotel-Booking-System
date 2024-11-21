using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Payment.Response;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Application.Services;
using HootelBooking.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Payment.Commands.ProccessPayment
{
    public class ProccessPaymentCommandHandler : IRequestHandler<ProccessPaymentCommand, Result<PaymentResponseDto>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly MessageService _messageService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ProccessPaymentCommandHandler(IPaymentRepository paymentRepository, IReservationRepository reservationRepository, MessageService messageService, IUserRepository userRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _reservationRepository = reservationRepository;
            _messageService = messageService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<PaymentResponseDto>> Handle(ProccessPaymentCommand request, CancellationToken cancellationToken)
        {
            
            var reservation = await _reservationRepository.GetReservationByIdAsync(request.Request.reservationId);

           
            if (reservation is null)
            {
                return new Result<PaymentResponseDto>(404, "Reservation Not Found");
            }

         
            if (reservation.ReservationStatusID != (int)enReservationStatus.PENDING)
            {
                return new Result<PaymentResponseDto>(400, "Reservation cannot be paid for in its current status.");
            }

            var payment = await _paymentRepository.AddPayment(reservation.Id, request.Request.Method, reservation.TotalPrice);

            if (payment is null )
            {
                throw new ErrorResponseException(500, "Operation Failed", "Internal server Error");
            }

            var proccessedPayment = await _paymentRepository.ProcesspaymentAsync(request.Request.reservationId, request.Request.Method);

         
            if (proccessedPayment is null)
            {
                throw new ErrorResponseException(500, "Operation Failed", "Internal server Error");
            }

            // Map the payment and return the success response immediately
            var mappedResult = _mapper.Map<PaymentResponseDto>(payment);
            
            var currentUser= await _userRepository.GetCurrentUserAsync();

           await _messageService.SendMessage("New Horizon Hotel", currentUser, payment.ReservationID, payment.Reservation.Room.RoomNumber, payment.Reservation.CheckInDate, payment.Reservation.CheckOutDate, payment.Reservation.NumberOfNights, payment.Price);

            return new Result<PaymentResponseDto>(mappedResult, 200, "Paid successfully");

        }
    }
}
