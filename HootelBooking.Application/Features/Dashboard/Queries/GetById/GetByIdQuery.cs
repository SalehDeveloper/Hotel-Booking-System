using HootelBooking.Application.Dtos.User.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Dashboard.Queries.GetById
{
    public class GetByIdQuery : IRequest<Result<UserResponseDto>>
    {
        public int Id { get; set; }
    }
}
