using HootelBooking.Application.Dtos.State.Request;
using HootelBooking.Application.Dtos.State.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.States.Commands.UpdateState
{
    public class UpdateStateCommand:IRequest<Result<StateResponseDto>>
    {
        public UpdateStateRequestDto State { get; set; }    
    }
}
