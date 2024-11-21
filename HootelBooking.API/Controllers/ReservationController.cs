using Hangfire;
using HootelBooking.API.Models;
using HootelBooking.Application.Dtos.Reservation.Request;
using HootelBooking.Application.Dtos.Reservation.Response;
using HootelBooking.Application.Dtos.Room.Response;
using HootelBooking.Application.Features.Reservation.Commands.CancelReservation;
using HootelBooking.Application.Features.Reservation.Commands.CheckIn;
using HootelBooking.Application.Features.Reservation.Commands.CheckOut;
using HootelBooking.Application.Features.Reservation.Commands.CreateReservation;
using HootelBooking.Application.Features.Reservation.Queries.GetActiveReservations;
using HootelBooking.Application.Features.Reservation.Queries.GetAllReservations;
using HootelBooking.Application.Features.Reservation.Queries.GetReservationByID;
using HootelBooking.Application.Features.Reservation.Queries.IsRoomAvailable;
using HootelBooking.Persistence.Jobs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HootelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<ReservationResponseDto>> CreateReservation([FromBody] CreateReservationRequestDto request)
        { 

            var result = await _mediator.Send(new CreateReservationCommand() { RequestDto = request });

            if (result.IsSuccess)

                return new ApiResponse<ReservationResponseDto>(result.Data, result.Message, (HttpStatusCode)result.Status);

            return new ApiResponse<ReservationResponseDto>((HttpStatusCode)result.Status, result.Message);

        }

        [HttpPut]
        [Route("Cancell/{reservationId}")]
        [Authorize]
        public async Task<ApiResponse<string>> CancellReservation([FromRoute] int reservationId)
        {

            var result = await _mediator.Send(new CancelReservationCommand() { ReservationId = reservationId });

            if (result.IsSuccess)
                return new ApiResponse<string>(result.Data, result.Message, (HttpStatusCode)result.Status);
            return new ApiResponse<string>((HttpStatusCode)result.Status, result.Message);
        }

        [HttpPut]
        [Route("CheckIn/{reservationId}")]
        [Authorize]
        public async Task<ApiResponse<ReservationResponseDto>> CheckIn(int reservationId)
        {
            var result = await _mediator.Send(new CheckInCommand() { ReservationId = reservationId });

            if (result.IsSuccess)
                return new ApiResponse<ReservationResponseDto>(result.Data, result.Message, (HttpStatusCode)result.Status);
            return new ApiResponse<ReservationResponseDto>((HttpStatusCode)result.Status, result.Message);

        }

        [HttpPut]
        [Route("CheckOut/{reservationId}")]
        [Authorize]
        public async Task<ApiResponse<ReservationResponseDto>> CheckOut(int reservationId)
        {
            var result = await _mediator.Send(new CheckOutCommand() { ReservationId = reservationId });

            if (result.IsSuccess)
                return new ApiResponse<ReservationResponseDto>(result.Data, result.Message, (HttpStatusCode)result.Status);
            return new ApiResponse<ReservationResponseDto>((HttpStatusCode)result.Status, result.Message);

        }


        [HttpGet]
        [Route("Active")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<IEnumerable<ReservationResponseDto>>> GetActiveReservations()
        {
            var res = await _mediator.Send(new GetActiveReservationsQuery());

            if (res.IsSuccess)
                return new ApiResponse<IEnumerable<ReservationResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<IEnumerable<ReservationResponseDto>>((HttpStatusCode)res.Status, res.Message);

        }



        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<IEnumerable<ReservationResponseDto>>> GetAllReservations()
        {
            var res = await _mediator.Send(new GetAllReservationQuery());

            if (res.IsSuccess)
                return new ApiResponse<IEnumerable<ReservationResponseDto>>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<IEnumerable<ReservationResponseDto>>((HttpStatusCode)res.Status, res.Message);

        }


        [HttpGet]
        [Route("id/{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<ReservationResponseDto>> GetReservationByID([FromRoute] int id)
        {
            var res = await _mediator.Send(new GetReservationByIDQuery() { ReservationID = id });

            if (res.IsSuccess)
                return new ApiResponse<ReservationResponseDto>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<ReservationResponseDto>((HttpStatusCode)res.Status, res.Message);


        }



        [HttpGet]
        [Route("RoomAvailablity")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ApiResponse<string>> IsRoomAvailable(  int roomId , DateTime CheckIn , DateTime CheckOut)
        { 
            var res = await _mediator.Send(new IsRoomAvailableQuery(roomId  , CheckIn, CheckOut));

            if (res.IsSuccess)
                return new ApiResponse<string>(res.Data, res.Message, (HttpStatusCode)res.Status);

            return new ApiResponse<string>((HttpStatusCode)res.Status, res.Message);


        }



    }
}
