using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Countries.Queries.GetById
{
    public class GetByIdQuery:IRequest<Result<CountryResponseDto>>
    {
        public int Id { get; set; }
    }
}
