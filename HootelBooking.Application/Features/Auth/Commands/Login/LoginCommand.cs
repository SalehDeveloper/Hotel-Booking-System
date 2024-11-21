using HootelBooking.Application.Dtos.Auth.Request;
using HootelBooking.Application.Dtos.Auth.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Auth.Commands.Login
{
    public class LoginCommand:IRequest<Result<loginResponseDto>>
    {
        public loginRequestDto Request {  get; set; }   
    }
}
