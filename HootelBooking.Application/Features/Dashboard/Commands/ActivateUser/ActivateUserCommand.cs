using HootelBooking.Application.Dtos.User.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Dashboard.Commands.ActivateUser
{
    public class ActivateUserCommand : IRequest<Result<ActivationUserResponseDto>>
    {
        public int Id { get; set; }
    }
}
