using HootelBooking.API.Models;
using HootelBooking.Application.Dtos.RoomType.Request;
using HootelBooking.Application.Dtos.RoomType.Response;
using HootelBooking.Application.Features.RoomTypes.Command.CreateRoomType;
using HootelBooking.Application.Features.RoomTypes.Command.DeleteRoomType;
using HootelBooking.Application.Features.RoomTypes.Command.UpdateRoomType;
using HootelBooking.Application.Features.RoomTypes.Query.GetActive;
using HootelBooking.Application.Features.RoomTypes.Query.GetAll;
using HootelBooking.Application.Features.RoomTypes.Query.GetById;
using HootelBooking.Application.Features.RoomTypes.Query.GetByType;
using HootelBooking.Application.Features.RoomTypes.Query.GetInActive;
using HootelBooking.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HootelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet]
        [Route("id/{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<RoomTypeResponseDto>> GetById([FromRoute] int id)
        {
            var res = await _mediator.Send(new GetByIdQuery() { Id = id });

            if (res.IsSuccess)
                return new ApiResponse<RoomTypeResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<RoomTypeResponseDto>((HttpStatusCode)res.Status, res.Message);


        }


        [HttpGet]
        [Route("type/{type}")]
        [AllowAnonymous]
        public async Task<ApiResponse<RoomTypeResponseDto>> GetByType([FromRoute] string type)
        {

            var res = await _mediator.Send(new GetByTypeQuery() { roomType = type });

            if (res.IsSuccess)
            {
                return new ApiResponse<RoomTypeResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<RoomTypeResponseDto>((HttpStatusCode)res.Status, res.Message);

        }

        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<IEnumerable<RoomTypeResponseDto>>> GetAll()
        {



            var res = await _mediator.Send(new GetAllQuery());

            if (res.IsSuccess)
                return new ApiResponse<IEnumerable<RoomTypeResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<IEnumerable<RoomTypeResponseDto>>((HttpStatusCode)res.Status, res.Message);






        }


        [HttpGet]
        [Route("Active")]
        [AllowAnonymous]
        public async Task<ApiResponse<IEnumerable<RoomTypeResponseDto>>> GetAllActive()
        {

            var res = await _mediator.Send(new GetActiveQuery());

            if (res.IsSuccess)
                return new ApiResponse<IEnumerable<RoomTypeResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<IEnumerable<RoomTypeResponseDto>>((HttpStatusCode)res.Status, res.Message);


        }


        [HttpGet]
        [Route("InActive")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<IEnumerable<RoomTypeResponseDto>>> GetAllInActive()
        {
            var res = await _mediator.Send(new GetInActiveQuery());

            if (res.IsSuccess)
                return new ApiResponse<IEnumerable<RoomTypeResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<IEnumerable<RoomTypeResponseDto>>((HttpStatusCode)res.Status, res.Message);

        }


        [HttpPost]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<RoomTypeResponseDto>> AddNew([FromBody] CreateRoomTypeRequestDto roomType)
        {
            var res = await _mediator.Send(new CreateRoomTypeCommand() { RoomType = roomType });

            if (res.IsSuccess)
            {
                return new ApiResponse<RoomTypeResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<RoomTypeResponseDto>((HttpStatusCode)res.Status, res.Message);


        }


        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<RoomTypeResponseDto>> Delete([FromRoute] int id)
        {
            var res = await _mediator.Send(new DeleteRoomTypeCommand() { Id = id });

            if (res.IsSuccess)
            {
                return new ApiResponse<RoomTypeResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<RoomTypeResponseDto>((HttpStatusCode)res.Status, res.Message);


        }

        [HttpPut]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<RoomTypeResponseDto>> Update([FromBody] UpdateRoomTypeRequestDto roomType)
        {

            var res = await _mediator.Send(new UpdateRoomTypeCommand() { RoomType = roomType });

            if (res.IsSuccess)
            {
                return new ApiResponse<RoomTypeResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }
            return new ApiResponse<RoomTypeResponseDto>((HttpStatusCode)res.Status, res.Message);
        }

    }
}
