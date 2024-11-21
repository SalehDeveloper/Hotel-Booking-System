using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Countries.Queries.GetCountryPopulationById
{
    public class GetPopulationByIdQuery:IRequest<Result<CountryPopulationResponseDto>>
    {
        public int Id { get; set; } 

    }
}
