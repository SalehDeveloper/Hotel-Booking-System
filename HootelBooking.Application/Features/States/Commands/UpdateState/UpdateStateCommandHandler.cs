using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.State.Response;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.States.Commands.UpdateState
{
    public class UpdateStateCommandHandler : IRequestHandler<UpdateStateCommand, Result<StateResponseDto>>
    { 
        private readonly IStateRepository _stateRepository; 
        private readonly IMapper _mapper;

        public UpdateStateCommandHandler(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        public async Task<Result<StateResponseDto>> Handle(UpdateStateCommand request, CancellationToken cancellationToken)
        {
            var stateToUpdate = await _stateRepository.GetByIdAsync(request.State.Id);
            
            if (stateToUpdate is not null) 
            {
                _mapper.Map(request.State, stateToUpdate);
                var res =await  _stateRepository.UpdatedAsync(stateToUpdate);

                if (res)
                {
                    var mappedResult = _mapper.Map<StateResponseDto>(stateToUpdate);

                    return new Result<StateResponseDto>(mappedResult, 200, "Updated Successfullt");
                }

                throw new ErrorResponseException(500, "Operation Failed", "Internal Server Error");

            }
            return new Result<StateResponseDto>(404, $"State With Id: {request.State.Id} Not Found");
            
        }
    }
}
