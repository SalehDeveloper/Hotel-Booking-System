using HootelBooking.API.Models;
using HootelBooking.Application.Dtos.State.Request;
using HootelBooking.Application.Dtos.State.Response;
using HootelBooking.Application.Features.States.Commands.AddState;
using HootelBooking.Application.Features.States.Commands.DeleteState;
using HootelBooking.Application.Features.States.Commands.UpdateState;
using HootelBooking.Application.Features.States.Queries.GetActiveStates;
using HootelBooking.Application.Features.States.Queries.GetAll;
using HootelBooking.Application.Features.States.Queries.GetById;
using HootelBooking.Application.Features.States.Queries.GetByName;
using HootelBooking.Application.Features.States.Queries.GetInActiveStates;
using HootelBooking.Application.Features.States.Queries.Search;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HootelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {    
        private readonly IMediator _mediator;

        public StateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("name/{name}")]
        public async Task<ApiResponse<StateResponseDto>> GetByName([FromRoute] string name)
        {

            var res = await _mediator.Send(new GetByNameQuery() { Name = name });

            if (res.IsSuccess)
            {
                return new ApiResponse<StateResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<StateResponseDto>((HttpStatusCode)res.Status, res.Message);

        }

        [HttpGet]
        [Route("id/{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<StateResponseDto>> GetById([FromRoute] int id)
        {
            var res = await _mediator.Send(new GetByIdQuery() { Id = id });

            if (res.IsSuccess)
                return new ApiResponse<StateResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<StateResponseDto>((HttpStatusCode)res.Status, res.Message);


        }

        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<PaginatedApiResponse<IEnumerable<StateResponseDto>>> GetAllPaginated(int pageNumber=1)
        {


            var res = await _mediator.Send(new GetAllQuery() { PageNumber = pageNumber });


            if (res.IsSuccess)
            {
                return new PaginatedApiResponse<IEnumerable<StateResponseDto>>
                    (res.Data, res.CurrentPage, res.TotalItems, res.PageSize, res.TotalPages, res.HasPerviousPage, res.HasNextPage, res.Message, (HttpStatusCode)res.Status);
            }

            return new PaginatedApiResponse<IEnumerable<StateResponseDto>>((HttpStatusCode)res.Status, res.Message);



        }


        [HttpGet]
        [Route("Active")]
        [AllowAnonymous]
        public async Task<PaginatedApiResponse<IEnumerable<StateResponseDto>>> GetAllActivePaginated(int pageNumber)
        {

            var res = await _mediator.Send(new GetActiveStatesQuery() { PageNumber = pageNumber });


            if (res.IsSuccess)
            {
                return new PaginatedApiResponse<IEnumerable<StateResponseDto>>
                    (res.Data, res.CurrentPage, res.TotalItems, res.PageSize, res.TotalPages, res.HasPerviousPage, res.HasNextPage, res.Message, (HttpStatusCode)res.Status);
            }

            return new PaginatedApiResponse<IEnumerable<StateResponseDto>>((HttpStatusCode)res.Status, res.Message);

        }


        [HttpGet]
        [Route("InActive")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<PaginatedApiResponse<IEnumerable<StateResponseDto>>> GetAllInActivePaginated(int pageNumber)
        {

            var res = await _mediator.Send(new GetInActiveStatesQuery() { PageNumber = pageNumber });


            if (res.IsSuccess)

                return new PaginatedApiResponse<IEnumerable<StateResponseDto>>
                    (res.Data, res.CurrentPage, res.TotalItems, res.PageSize, res.TotalPages, res.HasPerviousPage, res.HasNextPage, res.Message, (HttpStatusCode)res.Status);


            return new PaginatedApiResponse<IEnumerable<StateResponseDto>>((HttpStatusCode)res.Status, res.Message);

        }

        [HttpGet]
        [Route("Search/{keyword}")]
        [AllowAnonymous]
        public async Task<ApiResponse<IEnumerable<StateResponseDto>>> Search([FromRoute] string keyword)
        {

            var res = await _mediator.Send(new StateSearchQuery() { keywrod = keyword });

            if (res.IsSuccess)
                return new ApiResponse<IEnumerable<StateResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<IEnumerable<StateResponseDto>>((HttpStatusCode)res.Status, res.Message);


        }


        [HttpPost]
        [Authorize(Roles = "Admin, Owner")]

        public async Task<ApiResponse<StateResponseDto>> AddNew([FromBody] AddStateRequestDto state)
        {
            var res = await _mediator.Send(new AddStateCommand() { State = state });

            if (res.IsSuccess)
            {
                return new ApiResponse<StateResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<StateResponseDto>((HttpStatusCode)res.Status, res.Message);


        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<StateResponseDto>> Delete([FromRoute] int id)
        {
            var res = await _mediator.Send(new DeleteStateCommand() { StateId = id });

            if (res.IsSuccess)
            {
                return new ApiResponse<StateResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<StateResponseDto>((HttpStatusCode)res.Status, res.Message);


        }


        [HttpPut]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<StateResponseDto>> Update(UpdateStateRequestDto state)
        {

            var res = await _mediator.Send(new UpdateStateCommand() { State =  state});

            if (res.IsSuccess)
            {
                return new ApiResponse<StateResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }
            return new ApiResponse<StateResponseDto>((HttpStatusCode)res.Status, res.Message);
        }
    }
}
