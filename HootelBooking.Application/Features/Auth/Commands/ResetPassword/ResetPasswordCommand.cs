using HootelBooking.Application.Dtos.Auth.Request;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommand:IRequest<Result<string>>
    {
        public ResetPasswordRequestDto Request { get; set; }
    }
}
