using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Countries.Commands.DeleteCountry
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, Result<CountryResponseDto>>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public DeleteCountryCommandHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<Result<CountryResponseDto>> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var countryToDelete = await _countryRepository.GetByIdAsync(request.Id);

            if (countryToDelete != null)
            {
                var res = await _countryRepository.DeleteAsync(request.Id);

                var mappedCountry = _mapper.Map<CountryResponseDto>(countryToDelete);
                if (res == 0)
                    return new Result<CountryResponseDto>(mappedCountry, 409, "Country is Already delted ");

                if (res == request.Id)
                    return new Result<CountryResponseDto>(mappedCountry, 200, "Country Deleted Successfully");


            }
            return new Result<CountryResponseDto>(404, $"Country with Id: {request.Id} Not Found");




        }
    }
}
