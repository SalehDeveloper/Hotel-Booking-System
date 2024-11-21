using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.FeedBack.Response;
using HootelBooking.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.FeedBacks.Queries.GetRoomFeedbacks
{
    public class GetRoomFeedbacksQueryHandler : IRequestHandler<GetRoomFeedbacksQuery, Result<List<FeedBackResponseDto>>>
    {
        private readonly IFeedBackRepository _feedBackRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetRoomFeedbacksQueryHandler(IFeedBackRepository feedBackRepository, IRoomRepository roomRepository, IUserRepository userRepository, IMapper mapper)
        {
            _feedBackRepository = feedBackRepository;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<FeedBackResponseDto>>> Handle(GetRoomFeedbacksQuery request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetByIdAsync(request.RoomId);

            if (room == null)
                return new Result<List<FeedBackResponseDto>>(404, $"Room With ID: {request.RoomId} Not Found");

            var feedbacks = await _feedBackRepository.GetRoomFeedBacks(room.RoomId);

            if (feedbacks == null || !feedbacks.Any())
                return new Result<List<FeedBackResponseDto>>(404, "No FeedBacks Found");

            //var mappedFeedbacks = _mapper.Map<List<FeedBackResponseDto>>(feedbacks);
            var currentUser = await _userRepository.GetCurrentUserAsync();
            var mappedFeedbacks = feedbacks.Select(x => 
            {

                var feedback = _mapper.Map<FeedBackResponseDto>(x);
                feedback.UserName = currentUser.UserName;

                return feedback;
            
            }).ToList();

            return new Result<List<FeedBackResponseDto>>(mappedFeedbacks, 200, "Retrived Successfully");
        }
    }
}
