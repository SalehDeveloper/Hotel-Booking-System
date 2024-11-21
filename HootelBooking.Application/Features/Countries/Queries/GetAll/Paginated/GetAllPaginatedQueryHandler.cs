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

namespace HootelBooking.Application.Features.Countries.Queries.GetAll.Paginated
{
    public class GetAllPaginatedQueryHandler : IRequestHandler<GetAllPaginatedQuery, PaginatedResult<IEnumerable<CountryResponseDto>>>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public GetAllPaginatedQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<IEnumerable<CountryResponseDto>>> Handle(GetAllPaginatedQuery request, CancellationToken cancellationToken)
        {

            var countries = await _countryRepository.ListAllPaginatedAsync(request.PageNumber);

            if (countries.Item1.Any())
            {
                var result = _mapper.Map<List<CountryResponseDto>>(countries.Item1);

                return new PaginatedResult<IEnumerable<CountryResponseDto>>(result, request.PageNumber, countries.Item2, 200, "Retrived");

            }

            return new PaginatedResult<IEnumerable<CountryResponseDto>>(404, "No Countries Found");




        }
    }
}
