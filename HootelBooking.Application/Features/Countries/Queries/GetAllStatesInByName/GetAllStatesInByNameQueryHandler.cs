using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Models;
using MediatR;


namespace HootelBooking.Application.Features.Countries.Queries.GetAllStatesInByName
{
    public class GetAllStatesInByNameQueryHandler : IRequestHandler<GetAllStatesInByNameQuery, Result<StatesInCountryResponseDto>>
    {
        private readonly ICountryRepository _countryRepository ;
        private readonly IMapper _mapper;

        public GetAllStatesInByNameQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<Result<StatesInCountryResponseDto>> Handle(GetAllStatesInByNameQuery request, CancellationToken cancellationToken)
        {
            

            var isCountryFound =  await _countryRepository.GetByName(request.Name);

            if (isCountryFound is not null)
            {
                var states = await _countryRepository.GetAllStatesInByName(request.Name);

                if (states.Any())
                {

                    var mappedStates = _mapper.Map<IEnumerable<StateCountryResponseDto>>(states);

                    var result = new StatesInCountryResponseDto() { CountryName = request.Name  , States = mappedStates};

                    return new Result<StatesInCountryResponseDto>(result, 200, "retrieved Successfully");

                }

                return new Result<StatesInCountryResponseDto>(new StatesInCountryResponseDto() { CountryName=request.Name} , 204, $"Country With Name {request.Name} Has No States");
            }

            return new Result<StatesInCountryResponseDto>( 404, $"Country With Name {request.Name} Not Found");
        }
    }
}
