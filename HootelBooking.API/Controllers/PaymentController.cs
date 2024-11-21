using HootelBooking.API.Models;
using HootelBooking.Application.Dtos.Payment.Request;
using HootelBooking.Application.Dtos.Payment.Response;
using HootelBooking.Application.Features.Payment.Commands.ProccessPayment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HootelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    { 
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("ProccesPayment")]
        [Authorize]
        public async Task<ApiResponse<PaymentResponseDto>> ProccessPayment ([FromBody] ProccessPaymentRequestDto requestDto)
        {
            var result = await _mediator.Send(new ProccessPaymentCommand() { Request = requestDto });

            if(result.IsSuccess)
            {
                return new ApiResponse<PaymentResponseDto>(result.Data, result.Message, (HttpStatusCode)result.Status);
            }
            return new ApiResponse<PaymentResponseDto>((HttpStatusCode)result.Status, result.Message);

        }
    }
}
