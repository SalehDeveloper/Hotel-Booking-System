using HootelBooking.Application.Dtos.User.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Dashboard.Commands.DeActivateUser
{
    public class DeActivateUserCommand : IRequest<Result<ActivationUserResponseDto>>
    {
        public int Id { get; set; }
    }
}
