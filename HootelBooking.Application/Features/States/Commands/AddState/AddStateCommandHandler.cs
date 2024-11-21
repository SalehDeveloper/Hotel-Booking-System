using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.State.Response;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;

namespace HootelBooking.Application.Features.States.Commands.AddState
{
    public class AddStateCommandHandler : IRequestHandler<AddStateCommand, Result<StateResponseDto>>
    { 
        private readonly IStateRepository _stateRepository; 
        private readonly IMapper _mapper;

        public AddStateCommandHandler(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        public async  Task<Result<StateResponseDto>> Handle(AddStateCommand request, CancellationToken cancellationToken)
        {
            var stateToAdd = _mapper.Map<State>(request.State);

            var res = await _stateRepository.AddAsync(stateToAdd);

            if (res != null)
            {
               var mappedResult  = _mapper.Map<StateResponseDto>(res);
                return new Result<StateResponseDto>(mappedResult, 200, "Added Successfully");
            }

            return new Result<StateResponseDto>(500, "Internal server erro");
        }
    }
}
