using HootelBooking.Application.Dtos.User.Request;
using HootelBooking.Application.Dtos.User.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Dashboard.Commands.AddUser
{
    public class AddUserCommand : IRequest<Result<UserResponseDto>>
    {
        public AddUserRequestDto User { get; set; }
    }
}
