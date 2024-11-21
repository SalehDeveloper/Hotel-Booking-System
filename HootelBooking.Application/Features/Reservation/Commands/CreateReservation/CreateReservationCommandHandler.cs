using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Reservation.Response;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Reservation.Commands.CreateReservation
{
    internal class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Result<ReservationResponseDto>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        
        
        public CreateReservationCommandHandler(IReservationRepository reservationRepository, IRoomRepository roomRepository, IUserRepository userRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<ReservationResponseDto>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetByRoomNumber(request.RequestDto.RoomNumber);

            if (room is null)
            {
                return new Result<ReservationResponseDto>(404, $"Invalid Room with Number: {request.RequestDto.RoomNumber}");
            }

            if ( !await _reservationRepository.IsRoomAvailableAsync( room.RoomId  , request.RequestDto.CheckInDate  , request.RequestDto.CheckOutDate))
            {
                return new Result<ReservationResponseDto>(  404, "Room is Not Available at this Time");
            }

            if ( room.RoomStatusID == (int)enRoomStatus.UNDER_MAINTAINCE)
            {
                return new Result<ReservationResponseDto>(404, "Room Not Available at this time , it is Under Maintaince");
            }

            var currentUser = await _userRepository.GetCurrentUserAsync();

   
            var reservation = await _reservationRepository.CreateReservationAsync(
                room.RoomNumber,
                currentUser.Id,
                request.RequestDto.CheckInDate,
                request.RequestDto.CheckOutDate,
                request.RequestDto.NumberOfGuests
            );


     
            if (reservation is null)
            {
                throw new ErrorResponseException(500, "Creation Failed", "Internal server error");
            }

         
            var mappedResult = _mapper.Map<ReservationResponseDto>(reservation);
         
            

     
            return new Result<ReservationResponseDto>(mappedResult, 200, "Please Pay to Confirm your Reservation");
        }




    }
}
