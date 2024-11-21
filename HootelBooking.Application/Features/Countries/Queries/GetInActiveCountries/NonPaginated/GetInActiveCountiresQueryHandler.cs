using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Models;
using MediatR;

namespace HootelBooking.Application.Features.Countries.Queries.GetInActiveCountries.NonPaginated
{
    public class GetInActiveCountiresQueryHandler : IRequestHandler<GetInActiveCountriesQuery, Result<IEnumerable<CountryResponseDto>>>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public GetInActiveCountiresQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<CountryResponseDto>>> Handle(GetInActiveCountriesQuery request, CancellationToken cancellationToken)
        {
            var countries = await _countryRepository.GetInActiveCountries();

            if (countries.Any())

            {
                var result = _mapper.Map<List<CountryResponseDto>>(countries);

                return new Result<IEnumerable<CountryResponseDto>>(result, 200, "retrieved Successfully");


            }
            return new Result<IEnumerable<CountryResponseDto>>(404, $"No InActive Countries Found");



        }
    }
}
