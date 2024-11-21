using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Countries.Queries.GetCountriesPopulation
{
    public class GetPopulationQuery:IRequest<Result<IEnumerable<CountryPopulationResponseDto>>>
    {

    }
}
