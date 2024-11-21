using HootelBooking.API.Models;
using HootelBooking.Application.Dtos.Room.Request;
using HootelBooking.Application.Dtos.Room.Response;
using HootelBooking.Application.Features.Rooms.Commands.AddRoomPhotos;
using HootelBooking.Application.Features.Rooms.Commands.CreateRoom;
using HootelBooking.Application.Features.Rooms.Commands.DeleteRoom;
using HootelBooking.Application.Features.Rooms.Commands.DeleteRoomPhotos;
using HootelBooking.Application.Features.Rooms.Commands.UpdateRoom;
using HootelBooking.Application.Features.Rooms.Queries.GetActive;
using HootelBooking.Application.Features.Rooms.Queries.GetAll;
using HootelBooking.Application.Features.Rooms.Queries.GetByID;
using HootelBooking.Application.Features.Rooms.Queries.GetByPrice;
using HootelBooking.Application.Features.Rooms.Queries.GetByRoomNumber;
using HootelBooking.Application.Features.Rooms.Queries.GetByRoomType;
using HootelBooking.Application.Features.Rooms.Queries.GetInActive;
using HootelBooking.Application.Features.Rooms.Queries.GetRoomPhotos;
using HootelBooking.Application.Features.Rooms.Queries.Search;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HootelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Owner")]
    
        public async Task<ApiResponse<RoomResponseDto>> AddNew([FromForm] CreateRoomRequestDto room)
        {
            var res = await _mediator.Send(new CreateRoomCommand() { Request = room });

            if (res.IsSuccess)
            {
                return new ApiResponse<RoomResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<RoomResponseDto>((HttpStatusCode)res.Status, res.Message);


        }


        [HttpPut]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<RoomResponseDto>> Update([FromBody] UpdateRoomRequestDto room)
        {

            var res = await _mediator.Send(new UpdateRoomCommand() { Room = room });

            if (res.IsSuccess)
            {
                return new ApiResponse<RoomResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }
            return new ApiResponse<RoomResponseDto>((HttpStatusCode)res.Status, res.Message);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<RoomResponseDto>> Delete([FromRoute] int id)
        {
            var res = await _mediator.Send(new DeleteRoomCommand() { Id = id });

            if (res.IsSuccess)
            {
                return new ApiResponse<RoomResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<RoomResponseDto>((HttpStatusCode)res.Status, res.Message);


        }

        [HttpGet]
        [Route("Active")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<IEnumerable<RoomResponseDto>>> GetAllActive()
        {

            var res = await _mediator.Send(new GetActiveQuery());

            if (res.IsSuccess)
                return new ApiResponse<IEnumerable<RoomResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<IEnumerable<RoomResponseDto>>((HttpStatusCode)res.Status, res.Message);


        }


        [HttpGet]
        [Route("InActive")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<IEnumerable<RoomResponseDto>>> GetAllInActive()
        {
            var res = await _mediator.Send(new GetInActiveQuery());

            if (res.IsSuccess)
                return new ApiResponse<IEnumerable<RoomResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<IEnumerable<RoomResponseDto>>((HttpStatusCode)res.Status, res.Message);

        }

        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<IEnumerable<RoomResponseDto>>> GetAll()
        {



            var res = await _mediator.Send(new GetAllQuery());

            if (res.IsSuccess)
                return new ApiResponse<IEnumerable<RoomResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<IEnumerable<RoomResponseDto>>((HttpStatusCode)res.Status, res.Message);






        }


     
        [HttpGet]
        [Route("id/{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<RoomResponseDto>> GetById([FromRoute] int id)
        {
            var res = await _mediator.Send(new GetByIdQuery() { Id = id });

            if (res.IsSuccess)
                return new ApiResponse<RoomResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<RoomResponseDto>((HttpStatusCode)res.Status, res.Message);


        }


        [HttpGet]
        [Route("type/{roomType}")]
        [AllowAnonymous]
        public async Task<ApiResponse<IEnumerable<RoomResponseDto>>> GetByType([FromRoute] string roomType)
        {

            var res = await _mediator.Send(new GetByRoomTypeQuery() { RoomType = roomType });

            if (res.IsSuccess)
            {
                return new ApiResponse<IEnumerable<RoomResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<IEnumerable<RoomResponseDto>>((HttpStatusCode)res.Status, res.Message);

        }

        [HttpGet]
        [Route("price/{price}")]
        [AllowAnonymous]
        public async Task<ApiResponse<IEnumerable<RoomResponseDto>>> GetByPrice([FromRoute] decimal price)
        {

            var res = await _mediator.Send(new GetByPriceQuery() { Price = price });

            if (res.IsSuccess)
            {
                return new ApiResponse<IEnumerable<RoomResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<IEnumerable<RoomResponseDto>>((HttpStatusCode)res.Status, res.Message);

        }

        [HttpGet]
        [Route("roomNumber/{roomNumber}")]
        [AllowAnonymous]
        public async Task<ApiResponse<RoomResponseDto>> GetByRoomNumber([FromRoute] string roomNumber)
        {

            var res = await _mediator.Send(new GetByRoomNumberQuery() { RoomNumber = roomNumber });

            if (res.IsSuccess)
            {
                return new ApiResponse<RoomResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<RoomResponseDto>((HttpStatusCode)res.Status, res.Message);

        }


        [HttpGet]
        [Route("Filter")]
        [AllowAnonymous]
        public async Task<ApiResponse<IEnumerable<RoomResponseDto>>> Search( decimal? price , string? roomType , string? viewType , string? bedType)
        {

            var res = await _mediator.Send(new RoomSearchQuery(price , roomType , viewType , bedType ));

            if (res.IsSuccess)
            {
                return new ApiResponse<IEnumerable<RoomResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<IEnumerable<RoomResponseDto>>((HttpStatusCode)res.Status, res.Message);

        }




        [HttpGet]
        [Route("RoomPhotos/{roomId}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<IEnumerable<string>>> GetRoomPhotos(int roomId)
        {
            var photos = await _mediator.Send(new GetRoomPhotosQuery() { RoomId = roomId });

            if (photos.IsSuccess)
            {
                return new ApiResponse<IEnumerable<string>>(photos.Data ,  photos.Message, (HttpStatusCode)photos.Status);
            }
            return new ApiResponse<IEnumerable<string>>((HttpStatusCode)photos.Status , photos.Message);
        }



        [HttpPut]
        [Route("AddRoomPhotos")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<IEnumerable<string>>> AddRoomPhotos([FromForm] AddRoomPhotosRequestDto requestDto)
        {
            var photos = await _mediator.Send(new AddRoomPhotosCommand() {RequestDto  = requestDto });

            if (photos.IsSuccess)
            {
                return new ApiResponse<IEnumerable<string>>(photos.Data, photos.Message, (HttpStatusCode)photos.Status);
            }
            return new ApiResponse<IEnumerable<string>>((HttpStatusCode)photos.Status, photos.Message);
        }



        [HttpPut]
        [Route("DeleteRoomPhotos")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<IEnumerable<string>>> DeleteRoomPhotos([FromForm] DeleteRoomPhotosRequestDto requestDto)
        {
            var photos = await _mediator.Send(new DeleteRoomPhotosCommand() { RequestDto = requestDto });

            if (photos.IsSuccess)
            {
                return new ApiResponse<IEnumerable<string>>(photos.Data, photos.Message, (HttpStatusCode)photos.Status);
            }
            return new ApiResponse<IEnumerable<string>>((HttpStatusCode)photos.Status, photos.Message);
        }

    }
}
