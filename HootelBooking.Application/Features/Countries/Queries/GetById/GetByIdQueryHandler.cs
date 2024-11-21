using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Models;
using MediatR;


namespace HootelBooking.Application.Features.Countries.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery,Result< CountryResponseDto>>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<Result <CountryResponseDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {


            var country = await _countryRepository.GetByIdAsync(request.Id);
            if (country is not null)
            {
                var result = _mapper.Map<CountryResponseDto>(country);

                return new Result<CountryResponseDto>(result,200 ,"retrieved Successfully");
            }
          

            return new Result<CountryResponseDto>(404, $"Country Wiht Id: {request.Id} Not Found");



        }
    }
}
