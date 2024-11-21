using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Countries.Commands.UpdateCountry
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, Result<CountryResponseDto>>
    { 
        private readonly ICountryRepository _countryRepository; 
        private readonly IMapper _mapper;

        public UpdateCountryCommandHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<Result<CountryResponseDto>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {

            var countryToUpdate = await _countryRepository.GetByIdAsync(request.Country.Id);

            if (countryToUpdate != null)
            {
               
                _mapper.Map(request.Country, countryToUpdate);
                var result = await _countryRepository.UpdatedAsync( countryToUpdate);

                if (result )
                {
                    var mappedResult = _mapper.Map<CountryResponseDto>(countryToUpdate);
                    return new Result<CountryResponseDto>(mappedResult, 200, "Updated Successfully");
                }

                throw new ErrorResponseException(500, "Opeartion Failed", "Internal server Error");
            }
            return new Result<CountryResponseDto>(404, $"Country With Id {request.Country.Id} Not Found");
        }
    }
}
