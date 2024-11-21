using HootelBooking.Application.Dtos.Auth.Request;
using HootelBooking.Application.Dtos.Auth.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Auth.Commands.ConfirmTwoFactorCode
{
    public class ConfirmTwoFactorCodeCommand:IRequest<Result<loginResponseDto>>
    {
        public Confirm2FactorCodeRequestDto Request { get; set; }
    }
}
