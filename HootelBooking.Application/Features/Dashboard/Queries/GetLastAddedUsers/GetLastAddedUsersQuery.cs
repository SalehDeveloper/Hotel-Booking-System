using HootelBooking.Application.Dtos.Auth.Response;
using HootelBooking.Application.Dtos.User.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Dashboard.Queries.GetLastAddedUsers
{
    public class GetLastAddedUsersQuery:IRequest<Result<List<AuthResponseDto>>>
    {
        public int Days { get; set; }   
    }
}
