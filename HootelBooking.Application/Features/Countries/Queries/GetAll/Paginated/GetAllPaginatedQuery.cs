using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Models;
using MediatR;


namespace HootelBooking.Application.Features.Countries.Queries.GetAll.Paginated
{
    public class GetAllPaginatedQuery : IRequest<PaginatedResult<IEnumerable<CountryResponseDto>>>
    {
        

        public int PageNumber { get; set; }
    }
}
