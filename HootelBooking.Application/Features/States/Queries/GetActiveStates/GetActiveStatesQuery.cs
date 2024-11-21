using HootelBooking.Application.Dtos.State.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.States.Queries.GetActiveStates
{
    public class GetActiveStatesQuery: IRequest<PaginatedResult<IEnumerable<StateResponseDto>> >
    {
        public int PageNumber {  get; set; }    
    }
}
