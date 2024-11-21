using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Countries.Queries.GetByCode
{
    public class GetByCodeQuery:IRequest<Result <CountryResponseDto>>
    {
        public string Code {  get; set; }   
    }
}
