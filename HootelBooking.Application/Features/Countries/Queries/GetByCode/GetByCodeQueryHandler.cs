using AutoMapper;
using FluentValidation;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Features.Countries.Queries.GetById;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Countries.Queries.GetByCode
{
    public class GetByCodeQueryHandler : IRequestHandler<GetByCodeQuery, Result< CountryResponseDto>>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public GetByCodeQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task< Result<CountryResponseDto>> Handle(GetByCodeQuery request, CancellationToken cancellationToken)
        {
            
            var country =await  _countryRepository.GetByCode(request.Code);

            if (country is not null)

            {
                var result = _mapper.Map<CountryResponseDto>(country);
          
                 return new Result<CountryResponseDto>( result, 200,"retrieved Successfully");
            } 
            return  new Result<CountryResponseDto>(404, $"Country With Code {request.Code} Not Found");
        }
    }
}
