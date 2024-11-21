using HootelBooking.Application.Dtos.User.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Dashboard.Queries.GetByRole
{
    public class GetByRoleQuery : IRequest<Result<List<UserResponseDto>>>
    {
        public string Role { get; set; }
    }
}
