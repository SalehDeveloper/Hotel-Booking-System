using Hangfire;
using HootelBooking.API.Models;
using HootelBooking.Application.Dtos.Auth.Request;
using HootelBooking.Application.Dtos.Auth.Response;
using HootelBooking.Application.Features.Auth.Commands.ChangePassword;
using HootelBooking.Application.Features.Auth.Commands.ConfirmEmail;
using HootelBooking.Application.Features.Auth.Commands.ConfirmTwoFactorCode;
using HootelBooking.Application.Features.Auth.Commands.ForgotPassword;
using HootelBooking.Application.Features.Auth.Commands.Login;
using HootelBooking.Application.Features.Auth.Commands.Register;
using HootelBooking.Application.Features.Auth.Commands.ResetPassword;
using HootelBooking.Persistence.Jobs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HootelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {   
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<ApiResponse<AuthResponseDto>> Register([FromForm] RegisterRequestDto request)
        {
            var res = await _mediator.Send(new RegisterCommand() { Request = request });

            if (res.IsSuccess)
            {
                return new ApiResponse<AuthResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status); 
            }

            return new ApiResponse<AuthResponseDto>((HttpStatusCode)res.Status, res.Message);
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ApiResponse<loginResponseDto>> Login ([FromBody] loginRequestDto loginRequest)
        {
            var res = await _mediator.Send(new LoginCommand() { Request = loginRequest });

            if (res.IsSuccess)
            {
               

                return new ApiResponse<loginResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<loginResponseDto>((HttpStatusCode)res.Status, res.Message);

        }

        [HttpPost]
        [Route("EmailConfirmation")]
        [AllowAnonymous]
        public async Task<ApiResponse<string>> ConfirmEmail ([FromBody] ConfirmEmailRequestDto confirmEmailRequest)
        {
            var res = await _mediator.Send(new ConfirmEmailCommand() { Request = confirmEmailRequest });

            if (res.IsSuccess)
            {
                return new ApiResponse<string>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<string>((HttpStatusCode)res.Status, res.Message);
        }


        [HttpPost]
        [Route("TwoFactorCodeConfirmation")]
        [AllowAnonymous]
        public async Task<ApiResponse<loginResponseDto>> ConfirmTwoFactorCode([FromBody] Confirm2FactorCodeRequestDto confirmEmailRequest)
        {
            var res = await _mediator.Send(new ConfirmTwoFactorCodeCommand() { Request = confirmEmailRequest });

            if (res.IsSuccess)
            {
                return new ApiResponse<loginResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<loginResponseDto>((HttpStatusCode)res.Status, res.Message);
        }






        [HttpPost]
        [Route("ForgotPassword")]
        [AllowAnonymous]
        public async Task<ApiResponse<string>> ForgotPassword( string email)
        {
            var res = await _mediator.Send(new ForgotPasswordCommand() { Email = email });

            if (res.IsSuccess)
            {
                return new ApiResponse<string>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<string>((HttpStatusCode)res.Status, res.Message);

        }


        [HttpPost]
        [Route("ResetPassword")]
        [AllowAnonymous]
        public async Task<ApiResponse<string>> ResetPassword( [FromBody] ResetPasswordRequestDto resetPasswordRequest)
        {
            var res = await _mediator.Send(new ResetPasswordCommand() { Request = resetPasswordRequest });

            if (res.IsSuccess)
            {
                return new ApiResponse<string>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<string>((HttpStatusCode)res.Status, res.Message);

        }


        [HttpPost]
        [Route("ChangePassword")]
        [Authorize]
        public async Task<ApiResponse<string>> ChangePassword([FromBody] ChangePasswordRequestDto changePasswordRequest)
        {
            var res = await _mediator.Send(new ChangePasswordCommand() { Request = changePasswordRequest });

            if (res.IsSuccess)
            {
                return new ApiResponse<string>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<string>((HttpStatusCode)res.Status, res.Message);

        }



    }
}
