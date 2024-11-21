using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.State.Response;
using HootelBooking.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;

namespace HootelBooking.Application.Features.States.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<StateResponseDto>>
    { 

        private readonly IStateRepository _stateRepository; 
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        public async Task<Result<StateResponseDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _stateRepository.GetByIdAsync(request.Id);

            if (res is not null)
            { 
                var mappedResult  = _mapper.Map<StateResponseDto>(res); 
                return new Result<StateResponseDto>(mappedResult, 200, "Retrived Successfully");
            }

            return new Result<StateResponseDto>(404, $"State With Id: {request.Id} Not Found");


        }
    }
}
