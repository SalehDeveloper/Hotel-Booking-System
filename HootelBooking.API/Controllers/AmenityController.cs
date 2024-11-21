using HootelBooking.API.Models;
using HootelBooking.Application.Dtos.Amenity.Request;
using HootelBooking.Application.Dtos.Amenity.Response;
using HootelBooking.Application.Features.Amenities.Commands.CreateAmenity;
using HootelBooking.Application.Features.Amenities.Commands.DeleteAmenity;
using HootelBooking.Application.Features.Amenities.Commands.UpdateAmenity;
using HootelBooking.Application.Features.Amenities.Queries.GetActive;
using HootelBooking.Application.Features.Amenities.Queries.GetAll;
using HootelBooking.Application.Features.Amenities.Queries.GetById;
using HootelBooking.Application.Features.Amenities.Queries.GetByName;
using HootelBooking.Application.Features.Amenities.Queries.GetInActive;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HootelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class AmenityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AmenityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("name/{name}")]
        [AllowAnonymous]
        public async Task<ApiResponse<AmenityResponseDto>> GetByName([FromRoute] string name)
        {

            var res = await _mediator.Send(new GetByNameQuery() { Name = name });

            if (res.IsSuccess)
            {
                return new ApiResponse<AmenityResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<AmenityResponseDto>((HttpStatusCode)res.Status, res.Message);

        }

        [HttpGet]
        [Route("id/{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<AmenityResponseDto>> GetById([FromRoute] int id)
        {
            var res = await _mediator.Send(new GetByIdQuery() { Id = id });

            if (res.IsSuccess)
                return new ApiResponse<AmenityResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<AmenityResponseDto>((HttpStatusCode)res.Status, res.Message);


        }

        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<IEnumerable<AmenityResponseDto>>> GetAll()
        {



            var res = await _mediator.Send(new GetAllQuery());

            if (res.IsSuccess)
                return new ApiResponse<IEnumerable<AmenityResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<IEnumerable<AmenityResponseDto>>((HttpStatusCode)res.Status, res.Message);






        }


        [HttpGet]
        [Route("Active")]
        [AllowAnonymous]
        public async Task<ApiResponse<IEnumerable<AmenityResponseDto>>> GetAllActive()
        {

            var res = await _mediator.Send(new GetActiveAmenitiesQuery());

            if (res.IsSuccess)
                return new ApiResponse<IEnumerable<AmenityResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<IEnumerable<AmenityResponseDto>>((HttpStatusCode)res.Status, res.Message);


        }


        [HttpGet]
        [Route("InActive")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<IEnumerable<AmenityResponseDto>>> GetAllInActive()
        {
            var res = await _mediator.Send(new GetInActiveQuery());

            if (res.IsSuccess)
                return new ApiResponse<IEnumerable<AmenityResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<IEnumerable<AmenityResponseDto>>((HttpStatusCode)res.Status, res.Message);

        }


        [HttpPost]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<AmenityResponseDto>> AddNew([FromBody] CreateAmenityRequestDto amenity)
        {
            var res = await _mediator.Send(new CreateAmenityCommand() { Amenity = amenity });

            if (res.IsSuccess)
            {
                return new ApiResponse<AmenityResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<AmenityResponseDto>((HttpStatusCode)res.Status, res.Message);


        }


        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<AmenityResponseDto>> Delete([FromRoute] int id)
        {
            var res = await _mediator.Send(new DeleteAmenityCommand() { Id = id });

            if (res.IsSuccess)
            {
                return new ApiResponse<AmenityResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<AmenityResponseDto>((HttpStatusCode)res.Status, res.Message);


        }

        [HttpPut]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<AmenityResponseDto>> Update([FromBody]UpdateAmenityRequestDto amenity)
        {

            var res = await _mediator.Send(new UpdateAmenityCommand() { Amenity = amenity });

            if (res.IsSuccess)
            {
                return new ApiResponse<AmenityResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }
            return new ApiResponse<AmenityResponseDto>((HttpStatusCode)res.Status, res.Message);
        }

    }
}
