using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.State.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.States.Queries.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, PaginatedResult<IEnumerable<StateResponseDto>>>
    {

        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<IEnumerable<StateResponseDto>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var result = await _stateRepository.GetActiveStatesPaginatedAsync(request.PageNumber);
            
            if (result.Item1.Any() )
            {

                var mappedResult = _mapper.Map<IEnumerable<StateResponseDto>>(result.Item1);
               

                return new PaginatedResult<IEnumerable<StateResponseDto>>(mappedResult, request.PageNumber, result.Item2, 200, "Retrived Successfully");

            }
            
            return new PaginatedResult<IEnumerable<StateResponseDto>>(404, "No States Found");
        }
    }
}
