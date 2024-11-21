using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.FeedBack.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.FeedBacks.Queries.GetFeedbackByID
{
    public class GetFeedbackByIdQueryHandler : IRequestHandler<GetFeedbackByIdQuery, Result<FeedBackResponseDto>>
    {
        private readonly IFeedBackRepository _feedBackRepository;
        private readonly IUserRepository _userRepository;   
        private readonly IMapper _mapper;

        public GetFeedbackByIdQueryHandler(IFeedBackRepository feedBackRepository, IUserRepository userRepository, IMapper mapper)
        {
            _feedBackRepository = feedBackRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<FeedBackResponseDto>> Handle(GetFeedbackByIdQuery request, CancellationToken cancellationToken)
        {
            var feedback = await _feedBackRepository.GetByIdAsync(request.Id);

            if (feedback is not  null)
            {
                var currentUser = await _userRepository.GetCurrentUserAsync();
                var mappedFeedback = _mapper.Map<FeedBackResponseDto>(feedback);
                mappedFeedback.UserName = currentUser.UserName;

                return new Result<FeedBackResponseDto>(mappedFeedback, 200, "Retrived Successfully");
            }
            return new Result<FeedBackResponseDto>(404, $"FeedBack With ID: {request.Id} Not Found");

        }
    }
}
