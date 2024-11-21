
using HootelBooking.API.Models;
using HootelBooking.Application.Dtos.Country.Request;
using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Features.Countries.Commands.CreateCountry;
using HootelBooking.Application.Features.Countries.Commands.DeleteCountry;
using HootelBooking.Application.Features.Countries.Commands.UpdateCountry;
using HootelBooking.Application.Features.Countries.Queries.GetActiveCountries.NonPaginated;
using HootelBooking.Application.Features.Countries.Queries.GetActiveCountries.Paginated;
using HootelBooking.Application.Features.Countries.Queries.GetAll.NonPaginated;
using HootelBooking.Application.Features.Countries.Queries.GetAll.Paginated;
using HootelBooking.Application.Features.Countries.Queries.GetAllStatesInById;
using HootelBooking.Application.Features.Countries.Queries.GetAllStatesInByName;
using HootelBooking.Application.Features.Countries.Queries.GetByCode;
using HootelBooking.Application.Features.Countries.Queries.GetById;
using HootelBooking.Application.Features.Countries.Queries.GetByName;
using HootelBooking.Application.Features.Countries.Queries.GetCountriesPopulation;
using HootelBooking.Application.Features.Countries.Queries.GetCountryPopulationById;
using HootelBooking.Application.Features.Countries.Queries.GetInActiveCountries.NonPaginated;
using HootelBooking.Application.Features.Countries.Queries.GetInActiveCountries.Paginated;
using HootelBooking.Application.Features.Countries.Queries.Search;
using HootelBooking.Persistence.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace HootelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AppDbContext _context;
        public CountryController(IMediator mediator, AppDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }


        [HttpGet]
        [Route("code/{code}")]
        [AllowAnonymous]
        public async Task<ApiResponse<CountryResponseDto>> GetByCode([FromRoute] string code)
        {



            var res = await _mediator.Send(new GetByCodeQuery() { Code = code });

            if (res.IsSuccess)
            {
                return new ApiResponse<CountryResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<CountryResponseDto>((HttpStatusCode)res.Status, res.Message);


        }

        [HttpGet]
        [Route("name/{name}")]
        [AllowAnonymous]
        public async Task<ApiResponse<CountryResponseDto>> GetByName([FromRoute] string name)
        {

            var res = await _mediator.Send(new GetByNameQuery() { Name = name });

            if (res.IsSuccess)
            {
                return new ApiResponse<CountryResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<CountryResponseDto>((HttpStatusCode)res.Status, res.Message);

        }

        [HttpGet]
        [Route("id/{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<CountryResponseDto>> GetById([FromRoute] int id)
        {
            var res = await _mediator.Send(new GetByIdQuery() { Id = id });

            if (res.IsSuccess)
                return new ApiResponse<CountryResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<CountryResponseDto>((HttpStatusCode)res.Status, res.Message);


        }

        [HttpGet]
        [Route("AllPaginated")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<PaginatedApiResponse<IEnumerable<CountryResponseDto>>> GetAllPaginated(int pageNumber)
        {


            var res = await _mediator.Send(new GetAllPaginatedQuery() { PageNumber = pageNumber });


            if (res.IsSuccess)
            {
                return new PaginatedApiResponse<IEnumerable<CountryResponseDto>>
                    (res.Data, res.CurrentPage, res.TotalItems, res.PageSize, res.TotalPages, res.HasPerviousPage, res.HasNextPage, res.Message, (HttpStatusCode)res.Status);
            }

            return new PaginatedApiResponse<IEnumerable<CountryResponseDto>>((HttpStatusCode)res.Status, res.Message);



        }

        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<IEnumerable<CountryResponseDto>>> GetAll()
        {



            var res = await _mediator.Send(new GetAllQuery());

            if (res.IsSuccess)
                return new ApiResponse<IEnumerable<CountryResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<IEnumerable<CountryResponseDto>>((HttpStatusCode)res.Status, res.Message);






        }

        [HttpGet]
        [Route("Active")]
        [AllowAnonymous]
        public async Task<ApiResponse<IEnumerable<CountryResponseDto>>> GetAllActive()
        {

            var res = await _mediator.Send(new GetActiveCountriesQuery());

            if (res.IsSuccess)
                return new ApiResponse<IEnumerable<CountryResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<IEnumerable<CountryResponseDto>>((HttpStatusCode)res.Status, res.Message);


        }

        [HttpGet]
        [Route("ActivePaginated")]
        [AllowAnonymous]
        public async Task<PaginatedApiResponse<IEnumerable<CountryResponseDto>>> GetAllActivePaginated(int pageNumber)
        {

            var res = await _mediator.Send(new GetActiveCountriesPaginatedQuery() { PageNumber = pageNumber });


            if (res.IsSuccess)
            {
                return new PaginatedApiResponse<IEnumerable<CountryResponseDto>>
                    (res.Data, res.CurrentPage, res.TotalItems, res.PageSize, res.TotalPages, res.HasPerviousPage, res.HasNextPage, res.Message, (HttpStatusCode)res.Status);
            }

            return new PaginatedApiResponse<IEnumerable<CountryResponseDto>>((HttpStatusCode)res.Status, res.Message);

        }

        [HttpGet]
        [Route("InActive")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<IEnumerable<CountryResponseDto>>> GetAllInActive()
        {
            var res = await _mediator.Send(new GetInActiveCountriesQuery());

            if (res.IsSuccess)
                return new ApiResponse<IEnumerable<CountryResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<IEnumerable<CountryResponseDto>>((HttpStatusCode)res.Status, res.Message);

        }

        [HttpGet]
        [Route("InActivePaginated")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<PaginatedApiResponse<IEnumerable<CountryResponseDto>>> GetAllInActivePaginated(int pageNumber)
        {

            var res = await _mediator.Send(new GetInActiveCountriesPaginatedQuery() { PageNumber = pageNumber });


            if (res.IsSuccess)

                return new PaginatedApiResponse<IEnumerable<CountryResponseDto>>
                    (res.Data, res.CurrentPage, res.TotalItems, res.PageSize, res.TotalPages, res.HasPerviousPage, res.HasNextPage, res.Message, (HttpStatusCode)res.Status);


            return new PaginatedApiResponse<IEnumerable<CountryResponseDto>>((HttpStatusCode)res.Status, res.Message);

        }

        [HttpGet]
        [Route("States/{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<StatesInCountryResponseDto>> GetAllStatesInById([FromRoute] int id)
        {

            var res = await _mediator.Send(new GetAllStatesInByIdQuery() { Id = id });

            if (res.IsSuccess)
                return new ApiResponse<StatesInCountryResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<StatesInCountryResponseDto>((HttpStatusCode)res.Status, res.Message);

        }

        [HttpGet]
        [Route("AllStates/{name}")]
         [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<StatesInCountryResponseDto>> GetAllStatesInByName([FromRoute] string name)
        {
            var res = await _mediator.Send(new GetAllStatesInByNameQuery() { Name = name });

            if (res.IsSuccess)
                return new ApiResponse<StatesInCountryResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<StatesInCountryResponseDto>((HttpStatusCode)res.Status, res.Message);

        }


        [HttpGet]
        [Route("Population")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<IEnumerable<CountryPopulationResponseDto>>> GetCountriesPopulation()
        {



            var res = await _mediator.Send(new GetPopulationQuery());

            if (res.IsSuccess)
            {
                return new ApiResponse<IEnumerable<CountryPopulationResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<IEnumerable<CountryPopulationResponseDto>>((HttpStatusCode)res.Status, res.Message);


        }

        [HttpGet]
        [Route("Population/Id/{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<CountryPopulationResponseDto>> GetCountryPopulationById(int id)
        {

            var res = await _mediator.Send(new GetPopulationByIdQuery() { Id = id });

            if (res.IsSuccess)
                return new ApiResponse<CountryPopulationResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            return new ApiResponse<CountryPopulationResponseDto>((HttpStatusCode)res.Status, res.Message);

        }



        [HttpGet]
        [Route("Search/{keyword}")]
        [AllowAnonymous]
        public async Task<ApiResponse<IEnumerable<CountryResponseDto>>> Search([FromRoute] string keyword)
        {

            var res = await _mediator.Send(new SearchQuery() { keyword = keyword });

            if (res.IsSuccess)
                return new ApiResponse<IEnumerable<CountryResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<IEnumerable<CountryResponseDto>>((HttpStatusCode)res.Status, res.Message);


        }


        [HttpPost]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<CountryResponseDto>> AddNew([FromBody] CreateCountryRequestDto country)
        {
            var res = await _mediator.Send(new CreateCountryCommand() { country= country} );

            if (res.IsSuccess)
            {
                return new ApiResponse<CountryResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<CountryResponseDto>((HttpStatusCode)res.Status, res.Message);


        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<CountryResponseDto>> Delete([FromRoute] int id)
        {
            var res =await  _mediator.Send(new DeleteCountryCommand() { Id = id });

            if (res.IsSuccess)
            {
                return new ApiResponse<CountryResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<CountryResponseDto>((HttpStatusCode)res.Status , res.Message);   


        }

        [HttpPut]
        [Authorize(Roles = "Admin, Owner")]

        public async Task<ApiResponse<CountryResponseDto>> Update([FromBody]UpdateCountryRequestDto country)
        {
            
            var res = await _mediator.Send(new UpdateCountryCommand() { Country = country});

            if (res.IsSuccess)
            {
                return new ApiResponse<CountryResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status); 
            }
            return new ApiResponse<CountryResponseDto>((HttpStatusCode)res.Status, res.Message);
        }

        //[HttpPost]
        //[Route("AddStates")]
        //public async Task<ActionResult> AddStates(string countryCode)
        //{
        //    try
        //    {
        //        var client = new HttpClient();
        //        var userName = "Saleh.Dev"; // Your GeoNames username

        //        // Fetch country from the database
        //        var myCountry = await _context.Countries.FirstOrDefaultAsync(x => x.Code == countryCode);
        //        if (myCountry == null)
        //        {
        //            return NotFound($"Country with code {countryCode} not found.");
        //        }

        //        // Fetch country info from GeoNames
        //        var countryResponse = await client.GetAsync($"http://api.geonames.org/countryInfoJSON?country={countryCode}&username={userName}");

        //        if (!countryResponse.IsSuccessStatusCode)
        //        {
        //            return BadRequest("Unable to retrieve country info from GeoNames API.");
        //        }

        //        var countryContent = await countryResponse.Content.ReadAsStringAsync();
        //        var countryData = JsonConvert.DeserializeObject<GeoNamesResponse>(countryContent);

        //        if (countryData?.geonames?.Count > 0)
        //        {
        //            var firstGeoName = countryData.geonames[0]; // Use first geoname entry

        //            // Fetch states/children for the country using geonameId
        //            var stateResponse = await client.GetAsync($"http://api.geonames.org/childrenJSON?geonameId={firstGeoName.geonameId}&username={userName}");

        //            if (!stateResponse.IsSuccessStatusCode)
        //            {
        //                return BadRequest("Unable to retrieve states from GeoNames API.");
        //            }

        //            var stateContent = await stateResponse.Content.ReadAsStringAsync();
        //            var stateData = JsonConvert.DeserializeObject<Geonames>(stateContent);

        //            if (stateData?.GeonameList?.Count > 0)
        //            {
        //                // Add states to database
        //                var statesToAdd = stateData.GeonameList.Select(geoname => new Domain.Entities.State
        //                {
        //                    Name = geoname.Name,
        //                    IsActive = true,
        //                    CountryId = myCountry.Id
        //                }).ToList();

        //                _context.States.AddRange(statesToAdd);
        //                await _context.SaveChangesAsync();

        //                return Ok(stateData.GeonameList);
        //            }

        //            return NotFound("No states found for the country.");
        //        }

        //        return NotFound("No country data found.");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Consider logging the exception using Serilog for better traceability
        //        Log.Error(ex, "Error while fetching and adding states for country {CountryCode}", countryCode);
        //        return StatusCode(500, "An internal error occurred.");
        //    }
        //}
    }
}