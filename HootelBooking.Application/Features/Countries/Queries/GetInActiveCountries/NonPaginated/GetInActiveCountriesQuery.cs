using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Countries.Queries.GetInActiveCountries.NonPaginated
{
    public class GetInActiveCountriesQuery : IRequest<Result<IEnumerable<CountryResponseDto>>>
    {
        public int PageSize { get; set; }   

        public int PageNumber { get; set; } 
    }
}
