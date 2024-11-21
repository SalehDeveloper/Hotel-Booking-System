using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.State.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.States.Queries.GetByName
{
    public class GetByNameQueryHandler : IRequestHandler<GetByNameQuery, Result<StateResponseDto>>
    {

        private readonly IStateRepository _stateRepository; 
        private readonly IMapper _mapper;

        public GetByNameQueryHandler(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        public async Task<Result<StateResponseDto>> Handle(GetByNameQuery request, CancellationToken cancellationToken)
        {
            var res = await _stateRepository.GetByNameAsync(request.Name);

            if (res is not  null)
            {
                var mappedResult = _mapper.Map<StateResponseDto>(res);
                return new Result<StateResponseDto>(mappedResult, 200, "Retrived Successfully");
            }
            return new Result<StateResponseDto>(404, $"State Wiht Name {request.Name} Not Found");
        }
    }
}
