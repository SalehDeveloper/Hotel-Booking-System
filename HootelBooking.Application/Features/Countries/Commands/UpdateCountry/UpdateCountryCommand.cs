using HootelBooking.Application.Dtos.Country.Request;
using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Countries.Commands.UpdateCountry
{
    public class UpdateCountryCommand:IRequest<Result<CountryResponseDto>>
    {
        public UpdateCountryRequestDto Country { get; set; }    
    }
}
