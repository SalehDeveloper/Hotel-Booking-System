using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Models;
using MediatR;

namespace HootelBooking.Application.Features.Countries.Queries.GetAll.NonPaginated
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, Result<IEnumerable<CountryResponseDto>>>
    { 

        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CountryResponseDto>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {

            var countries = await _countryRepository.ListAllAsync();

            if (countries.Any())
            { 
                var mappedCountries = _mapper.Map<IEnumerable<CountryResponseDto>>(countries);
                return new Result<IEnumerable<CountryResponseDto>>(mappedCountries, 200, "Retrived Successfully");
            }

            return new Result<IEnumerable<CountryResponseDto>>(404, "No Countries Found");


        }
    }
}
