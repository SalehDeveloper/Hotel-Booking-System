using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Models;
using MediatR;


namespace HootelBooking.Application.Features.Countries.Queries.GetAllStatesInById
{
    public class GetAllStatesInByIdQueryHandler : IRequestHandler<GetAllStatesInByIdQuery, Result<StatesInCountryResponseDto>>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public GetAllStatesInByIdQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<Result<StatesInCountryResponseDto>> Handle(GetAllStatesInByIdQuery request, CancellationToken cancellationToken)
        {
            

            var isCountryFound = await _countryRepository.GetByIdAsync(request.Id);

            if (isCountryFound is not null)
            { 
                //get all states
                var statesInCountry = await _countryRepository.GetAllStatesInById(request.Id);
              
              

  
                //check if any states exist 
                if (statesInCountry.Any())
                {
                    var mappedStates = _mapper.Map<IEnumerable<StateCountryResponseDto>>(statesInCountry);
                    var result = new StatesInCountryResponseDto() { CountryName = isCountryFound.Name, States = mappedStates };
                    return new Result<StatesInCountryResponseDto>(result, 200, "retrieved Successfully");
                }

                return new Result<StatesInCountryResponseDto>( new StatesInCountryResponseDto() { CountryName=isCountryFound.Name} , 204, $"Country With Id {request.Id} has No States");

            }

            return new Result<StatesInCountryResponseDto>( 404, $"Country With Id {request.Id} Not Found");
        }
    }
}
