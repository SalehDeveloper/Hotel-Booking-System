using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommand:IRequest<Result<string>>
    {
        public string Email { get; set; }   
    }
}
