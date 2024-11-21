using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Countries.Commands.CreateCountry
{
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, Result<CountryResponseDto>>
    { 
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CreateCountryCommandHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<Result<CountryResponseDto>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var countryToAdd = _mapper.Map<Country>(request.country);

            var result = await _countryRepository.AddAsync(countryToAdd);

            if (result is not null )
            {
                var mappedResult = _mapper.Map<CountryResponseDto>(result);

                return new Result<CountryResponseDto>(mappedResult, 200, "Added Successfully");
            }

            return new Result<CountryResponseDto>(500, "Internal server error");
           
        }
    }
}
