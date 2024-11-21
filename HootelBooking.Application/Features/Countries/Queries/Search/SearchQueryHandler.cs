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

namespace HootelBooking.Application.Features.Countries.Queries.Search
{
    public class SearchQueryHandler : IRequestHandler<SearchQuery, Result<IEnumerable<CountryResponseDto>>>
    {
        private readonly ICountryRepository _countryRepository;

        private readonly IMapper _mapper;

        public SearchQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<Result <IEnumerable<CountryResponseDto>>> Handle(SearchQuery request, CancellationToken cancellationToken)
        {

            var countries = await _countryRepository.Search(request.keyword);

            if (countries.Any())
            {
               var result =   _mapper.Map<IEnumerable<CountryResponseDto>>(countries);

                return new Result<IEnumerable<CountryResponseDto>>(result, 200, "retrieved Successfully");
            }
            return new Result<IEnumerable<CountryResponseDto>>(404, $"No Countries Found ");
        }
    }
}
