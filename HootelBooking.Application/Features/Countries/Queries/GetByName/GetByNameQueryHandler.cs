using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Models;
using MediatR;


namespace HootelBooking.Application.Features.Countries.Queries.GetByName
{
    public class GetByNameQueryHandler : IRequestHandler<GetByNameQuery, Result< CountryResponseDto>>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public GetByNameQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<Result<CountryResponseDto>> Handle(GetByNameQuery request, CancellationToken cancellationToken)
        { 
           

            

            var country = await _countryRepository.GetByName(request.Name);

            if (country is not null)

            {
              var result =    _mapper.Map<CountryResponseDto>(country);

                return new Result<CountryResponseDto>(result, 200, "retrieved Successfully");
            }
            return new Result<CountryResponseDto>(404, $"Country With Name {request.Name} Not Found");


        }
    }
}
