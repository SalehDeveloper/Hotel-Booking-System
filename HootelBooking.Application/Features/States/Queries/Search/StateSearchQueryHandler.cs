using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.State.Response;
using HootelBooking.Application.Models;
using MediatR;

namespace HootelBooking.Application.Features.States.Queries.Search
{
    public class StateSearchQueryHandler : IRequestHandler<StateSearchQuery, Result<IEnumerable<StateResponseDto>>>
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;

        public StateSearchQueryHandler(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<StateResponseDto>>> Handle(StateSearchQuery request, CancellationToken cancellationToken)
        {
            var res = await _stateRepository.SearchAsync(request.keywrod);

            if (res.Any() )
            {
                var mappedResult = _mapper.Map<IEnumerable<StateResponseDto>>(res);

                return new Result<IEnumerable<StateResponseDto>>(mappedResult, 200, "Retrived Successfully");
            }

            return new Result<IEnumerable<StateResponseDto>>(404, "No States Found");
        }
    }
}
