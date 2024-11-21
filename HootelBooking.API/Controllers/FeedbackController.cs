using HootelBooking.API.Models;
using HootelBooking.Application.Dtos.Amenity.Request;
using HootelBooking.Application.Dtos.Amenity.Response;
using HootelBooking.Application.Dtos.FeedBack.Request;
using HootelBooking.Application.Dtos.FeedBack.Response;
using HootelBooking.Application.Features.Amenities.Commands.CreateAmenity;
using HootelBooking.Application.Features.Amenities.Commands.DeleteAmenity;
using HootelBooking.Application.Features.Amenities.Commands.UpdateAmenity;
using HootelBooking.Application.Features.FeedBacks.Commands.AddFeedback;
using HootelBooking.Application.Features.FeedBacks.Commands.DeleteFeedback;
using HootelBooking.Application.Features.FeedBacks.Commands.UpdateFeedback;
using HootelBooking.Application.Features.FeedBacks.Queries.GetFeedbackByID;
using HootelBooking.Application.Features.FeedBacks.Queries.GetRoomFeedbacks;
using HootelBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace HootelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {  
       private readonly IMediator _mediator;

        public FeedbackController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("id/{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<FeedBackResponseDto>> GetById([FromRoute] int id)
        {
            var res = await _mediator.Send(new GetFeedbackByIdQuery() { Id = id });

            if (res.IsSuccess)
                return new ApiResponse<FeedBackResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<FeedBackResponseDto>((HttpStatusCode)res.Status, res.Message);


        }

        [HttpGet]
        [Route("RoomFeedbacks/{roomId}")]
        [Authorize]
        public async Task<ApiResponse<List<FeedBackResponseDto>>> GetRoomFeedbacks([FromRoute] int roomId)
        {
            var feedbacks = await _mediator.Send(new GetRoomFeedbacksQuery() { RoomId = roomId });

            if (feedbacks.IsSuccess)
                return new ApiResponse<List<FeedBackResponseDto>>(feedbacks.Data, feedbacks.Message, (HttpStatusCode)feedbacks.Status);
           
            return new ApiResponse<List<FeedBackResponseDto>>((HttpStatusCode)feedbacks.Status, feedbacks.Message);

        }

        [HttpPost]
        [Authorize]
        public async Task<ApiResponse<FeedBackResponseDto>> AddFeedback([FromBody] AddFeedbackRequestDto feedback)
        {
            var res = await _mediator.Send(new AddFeedbackCommand() { Request = feedback });

            if (res.IsSuccess)
            {
                return new ApiResponse<FeedBackResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<FeedBackResponseDto>((HttpStatusCode)res.Status, res.Message);


        }


        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<ApiResponse<string>> Delete([FromRoute] int id)
        {
            var res = await _mediator.Send(new DeleteFeedbackCommand() { FeedbackId = id });

            if (res.IsSuccess)
            {
                return new ApiResponse<string>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }

            return new ApiResponse<string>((HttpStatusCode)res.Status, res.Message);


        }

        [HttpPut]
        [Authorize]
        public async Task<ApiResponse<FeedBackResponseDto>> Update([FromBody] UpdateFeedbackRequestDto feedback)
        {

            var res = await _mediator.Send(new UpdateFeedbackCommand() { Request = feedback });

            if (res.IsSuccess)
            {
                return new ApiResponse<FeedBackResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);
            }
            return new ApiResponse<FeedBackResponseDto>((HttpStatusCode)res.Status, res.Message);
        }

    }
}
