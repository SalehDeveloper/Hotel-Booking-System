using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.State.Response;
using HootelBooking.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.States.Queries.GetInActiveStates
{
    public class GetInActiveStatesQueryHandler : IRequestHandler<GetInActiveStatesQuery, PaginatedResult<IEnumerable<StateResponseDto>>>
    {
        private readonly IStateRepository _stateRepository; 
        private readonly IMapper _mapper;

        public GetInActiveStatesQueryHandler(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<IEnumerable<StateResponseDto>>> Handle(GetInActiveStatesQuery request, CancellationToken cancellationToken)
        {
            var res = await _stateRepository.GetInActiveStatesPaginatedAsync(request.PageNumber);

            if (res.Item1.Any() )
            {
                var mappedResult = _mapper.Map<IEnumerable<StateResponseDto>>(res.Item1);
                return new PaginatedResult<IEnumerable<StateResponseDto>>(mappedResult, request.PageNumber, res.Item2, 200, "Retrived Suucessfully");
            }

            return new PaginatedResult<IEnumerable<StateResponseDto>>(404, "No InActive States Found");
        }
    }
}
