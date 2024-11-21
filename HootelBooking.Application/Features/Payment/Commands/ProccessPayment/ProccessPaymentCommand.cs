using HootelBooking.Application.Dtos.Payment.Request;
using HootelBooking.Application.Dtos.Payment.Response;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Payment.Commands.ProccessPayment
{
    public class ProccessPaymentCommand : IRequest<Result<PaymentResponseDto>>
    {
        public ProccessPaymentRequestDto Request { get; set; }
    }
}
