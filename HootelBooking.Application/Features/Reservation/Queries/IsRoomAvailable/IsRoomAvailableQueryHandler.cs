using HootelBooking.Application.Contracts;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Reservation.Queries.IsRoomAvailable
{
    public class IsRoomAvailableQueryHandler : IRequestHandler<IsRoomAvailableQuery, Result<string>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;

        public IsRoomAvailableQueryHandler(IReservationRepository reservationRepository, IRoomRepository roomRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
        }

        public async Task<Result<string>> Handle(IsRoomAvailableQuery request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetByIdAsync(request.Id);

            if (room == null)
                return new Result<string>(404, $"Room with Id: {request.Id} Not Found");

            var isRoomAvailable = await _reservationRepository.IsRoomAvailableAsync(request.Id, request.CheckIn, request.CheckOut);

            if (isRoomAvailable)
                return new Result<string>($"Room is Available For this Date CheckIn: {request.CheckIn}  CheckOut: {request.CheckOut}", 200, "Done Successfully");



            return new Result<string>("Room is Not Available For This Time ", 200, "Done Successfully");
        }
    }
}
