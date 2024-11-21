using HootelBooking.API.Models;
using HootelBooking.Application.Dtos.Auth.Response;
using HootelBooking.Application.Dtos.User.Request;
using HootelBooking.Application.Dtos.User.Response;
using HootelBooking.Application.Features.Dashboard.Commands.ActivateUser;
using HootelBooking.Application.Features.Dashboard.Commands.AddUser;
using HootelBooking.Application.Features.Dashboard.Commands.ChangeUserRole;
using HootelBooking.Application.Features.Dashboard.Commands.DeActivateUser;
using HootelBooking.Application.Features.Dashboard.Queries.GetActive;
using HootelBooking.Application.Features.Dashboard.Queries.GetAll;
using HootelBooking.Application.Features.Dashboard.Queries.GetByEmail;
using HootelBooking.Application.Features.Dashboard.Queries.GetById;
using HootelBooking.Application.Features.Dashboard.Queries.GetByRole;
using HootelBooking.Application.Features.Dashboard.Queries.GetInActive;
using HootelBooking.Application.Features.Dashboard.Queries.GetLastAddedUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HootelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<UserResponseDto>> AddUser([FromBody] AddUserRequestDto request)
        {
            var res = await _mediator.Send(new AddUserCommand() { User = request });

            if (res.IsSuccess)
            {
                return new ApiResponse<UserResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<UserResponseDto>((HttpStatusCode)res.Status, res.Message);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<ActivationUserResponseDto>> DeActivate([FromRoute] int id)
        {
            var res = await _mediator.Send(new DeActivateUserCommand() { Id = id });

            if (res.IsSuccess)
            {
                return new ApiResponse<ActivationUserResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<ActivationUserResponseDto>((HttpStatusCode)res.Status, res.Message);


        }

        [HttpPut]
        [Route("Activate/{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<ActivationUserResponseDto>> Activate([FromRoute] int id)
        {
            var res = await _mediator.Send(new ActivateUserCommand() { Id = id });

            if (res.IsSuccess)
            {
                return new ApiResponse<ActivationUserResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<ActivationUserResponseDto>((HttpStatusCode)res.Status, res.Message);


        }

        [HttpPut]
        [Route("ChangeRole")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<ChangeUserRoleResponseDto>> ChangeRole([FromBody] ChangeUserRoleRequestDto request)
        {
            var res = await _mediator.Send(new ChangeUserRoleCommand() { RequestDto = request});

            if (res.IsSuccess)
            {
                return new ApiResponse<ChangeUserRoleResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<ChangeUserRoleResponseDto>((HttpStatusCode)res.Status, res.Message);


        }


        [HttpGet]
        [Route("id/{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<UserResponseDto>> GetById([FromRoute] int id)
        {
            var res = await _mediator.Send(new GetByIdQuery() { Id = id });

            if (res.IsSuccess)
                return new ApiResponse<UserResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<UserResponseDto>((HttpStatusCode)res.Status, res.Message);


        }

        [HttpGet]
        [Route("email/{email}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<UserResponseDto>> GetByEmail([FromRoute] string email)
        {
            var res = await _mediator.Send(new GetByEmailQuery() { Email = email });

            if (res.IsSuccess)
                return new ApiResponse<UserResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<UserResponseDto>((HttpStatusCode)res.Status, res.Message);


        }

        [HttpGet]
        [Route("role/{role}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<List<UserResponseDto>>> GetByRole([FromRoute] string role)
        {
            var res = await _mediator.Send(new GetByRoleQuery() { Role = role });

            if (res.IsSuccess)
                return new ApiResponse<List<UserResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<List<UserResponseDto>>((HttpStatusCode)res.Status, res.Message);


        }


        [HttpGet]
        [Route("Active")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<List<UserResponseDto>>> GetActive()
        {
            var res = await _mediator.Send(new GetActiveQuery() );

            if (res.IsSuccess)
                return new ApiResponse<List<UserResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<List<UserResponseDto>>((HttpStatusCode)res.Status, res.Message);


        }


        [HttpGet]
        [Route("InActive")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<List<UserResponseDto>>> GetInActive()
        {
            var res = await _mediator.Send(new GetInActiveQuery());

            if (res.IsSuccess)
                return new ApiResponse<List<UserResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<List<UserResponseDto>>((HttpStatusCode)res.Status, res.Message);


        }


        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<List<UserResponseDto>>> GetAll()
        {
            var res = await _mediator.Send(new GetAllQuery());

            if (res.IsSuccess)
                return new ApiResponse<List<UserResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<List<UserResponseDto>>((HttpStatusCode)res.Status, res.Message);


        }

        [HttpGet]
        [Route("LastAdded/{days}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<List<AuthResponseDto>>> GetLastAddedUsers([FromRoute] int days)
        {
            var res = await _mediator.Send(new GetLastAddedUsersQuery() { Days = days});

            if (res.IsSuccess)
                return new ApiResponse<List<AuthResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<List<AuthResponseDto>>((HttpStatusCode)res.Status, res.Message);


        }







    }
}
