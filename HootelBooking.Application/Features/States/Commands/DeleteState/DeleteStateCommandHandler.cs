using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Dtos.State.Response;
using HootelBooking.Application.Models;
using MediatR;


namespace HootelBooking.Application.Features.States.Commands.DeleteState
{
    public class DeleteStateCommandHandler : IRequestHandler<DeleteStateCommand, Result<StateResponseDto>>
    { 
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;

        public DeleteStateCommandHandler(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        public async Task<Result<StateResponseDto>> Handle(DeleteStateCommand request, CancellationToken cancellationToken)
        {

            var stateToDelete = await _stateRepository.GetByIdAsync(request.StateId);

            if (stateToDelete != null)
            {
                var res = await _stateRepository.DeleteAsync(request.StateId);

                var mappedCountry = _mapper.Map<StateResponseDto>(stateToDelete);
                if (res == 0)
                    return new Result<StateResponseDto>(mappedCountry, 409, "State is Already delted ");

                if (res == request.StateId)
                    return new Result<StateResponseDto>(mappedCountry, 200, "State Deleted Successfully");


            }
            return new Result<StateResponseDto>(404, $"State with Id: {request.StateId} Not Found");
        }
    }
}
