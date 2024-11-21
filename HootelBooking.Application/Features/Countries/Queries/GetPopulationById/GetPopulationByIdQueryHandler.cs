using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Features.Countries.Queries.GetCountryPopulationById;
using HootelBooking.Application.Models;
using MediatR;

namespace HootelBooking.Application.Features.Countries.Queries.GetPopulationById
{
    public class GetPopulationByIdQueryHandler : IRequestHandler<GetPopulationByIdQuery, Result<CountryPopulationResponseDto>>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public GetPopulationByIdQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<Result<CountryPopulationResponseDto>> Handle(GetPopulationByIdQuery request, CancellationToken cancellationToken)
        {
           
            var countryPopulation = await _countryRepository.GetCountriesPopulationById(request.Id);

            // this means country found 
            if (countryPopulation.Key is not null )
            {
                var result = new CountryPopulationResponseDto()
                {
                    Name = countryPopulation.Key.Name,
                    Population = countryPopulation.Value
                };

                return new Result<CountryPopulationResponseDto>(result, 200, "Retrived Successfully");

            }

            return new Result<CountryPopulationResponseDto>(404, $"Country With Id {request.Id} Not Found");
                



        }

      
    }
}
