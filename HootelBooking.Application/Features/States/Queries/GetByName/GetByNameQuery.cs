using HootelBooking.Application.Dtos.State.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.States.Queries.GetByName
{
    public class GetByNameQuery:IRequest<Result<StateResponseDto>>
    {
        public string Name { get; set; }        
    }
}
