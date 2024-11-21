using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Countries.Queries.GetCountriesPopulation
{
    public class GetPopulationQueryHandler : IRequestHandler<GetPopulationQuery, Result<IEnumerable<CountryPopulationResponseDto>>>
    { 

        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public GetPopulationQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CountryPopulationResponseDto>>> Handle(GetPopulationQuery request, CancellationToken cancellationToken)
        {
            var countriesPopulation = await _countryRepository.GetCountriesPopulation();
            // Map the dictionary to List of CountryPopulationResponseDto

            var result = countriesPopulation
                         .Select
                         (x => new CountryPopulationResponseDto
                         {
                             Name = x.Key.Name,
                             Population = x.Value

                         }
                         ).ToList();

            if (result.Any())
                return new Result<IEnumerable<CountryPopulationResponseDto>>(result, 200, "Retrived Successfully");

            return new Result<IEnumerable<CountryPopulationResponseDto>>(404, "No Countries");
        }
    }
}
