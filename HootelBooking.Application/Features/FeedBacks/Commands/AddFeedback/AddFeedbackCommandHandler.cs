using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.FeedBack.Response;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using MediatR;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.FeedBacks.Commands.AddFeedback
{
    public class AddFeedbackCommandHandler : IRequestHandler<AddFeedbackCommand, Result<FeedBackResponseDto>>
    {
        private readonly IFeedBackRepository  _feedBackRepository;
        private readonly IUserRepository _userRepository;
        private readonly IReservationRepository _reservationRepository; 
        private readonly IMapper _mapper;

        public AddFeedbackCommandHandler(IFeedBackRepository feedBackRepository, IUserRepository userRepository, IReservationRepository reservationRepository, IMapper mapper)
        {
            _feedBackRepository = feedBackRepository;
            _userRepository = userRepository;
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<Result<FeedBackResponseDto>> Handle(AddFeedbackCommand request, CancellationToken cancellationToken)
        {    
            
            var reservation = await _reservationRepository.GetReservationByIdAsync(request.Request.ReservationID);

            if (reservation == null)
                return new Result<FeedBackResponseDto>(404, "Reservation Not Found");

            if (reservation.ReservationStatusID == (int)enReservationStatus.CANCELED)
                return new Result<FeedBackResponseDto>(403, "you cannot gave your feedback for a cancelled reservation");

            if (reservation.ReservationStatusID != (int)enReservationStatus.COMPLETED)
                return new Result<FeedBackResponseDto>(403, "you cannot leave feedback before the reservation end");


            var currentUser = await _userRepository.GetCurrentUserAsync();
            bool isReservationForUser = reservation.UserID == currentUser.Id;

            if (!isReservationForUser)
                return new Result<FeedBackResponseDto>(403, "user cannot gave his feedBack for this reservation");

            var feedBack = new FeedBack()
            {
                UserID = currentUser.Id,
                ReservationID = request.Request.ReservationID,
                Rate = request.Request.Rate,
                Comment = request.Request.Comment,
                CreatedAt = DateTime.UtcNow,

            };

             await  _feedBackRepository.AddAsync(feedBack);

            var mappedFeedback = _mapper.Map<FeedBackResponseDto>(feedBack);
            mappedFeedback.UserName = currentUser.UserName;

            return new Result<FeedBackResponseDto>(mappedFeedback, 200, "Added Successfully");


        }
    }
}
