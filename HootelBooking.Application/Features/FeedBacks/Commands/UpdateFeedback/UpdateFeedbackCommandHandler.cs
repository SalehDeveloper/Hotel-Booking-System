using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.FeedBack.Response;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.FeedBacks.Commands.UpdateFeedback
{
    public class UpdateFeedbackCommandHandler : IRequestHandler<UpdateFeedbackCommand, Result<FeedBackResponseDto>>
    {
        private readonly IFeedBackRepository _feedBackRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateFeedbackCommandHandler(IFeedBackRepository feedBackRepository, IUserRepository userRepository, IMapper mapper)
        {
            _feedBackRepository = feedBackRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<FeedBackResponseDto>> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
        {
            var feedback = await _feedBackRepository.GetByIdAsync(request.Request.FeedbackID);

            if (feedback == null)
                return new Result<FeedBackResponseDto>(404, "FeedBack Not Found");

            var currentUser = await _userRepository.GetCurrentUserAsync();

            var isFeedbackforUser = feedback.UserID == currentUser.Id; 

            if (!isFeedbackforUser)
                return new Result<FeedBackResponseDto>(403, "You cannot Update this FeedBack");


            feedback.Comment = request.Request.Comment;
            feedback.Rate = request.Request.Rate;   

           var updatedFeedback =  await _feedBackRepository.UpdatedAsync(feedback);
           var mappedFeedback = _mapper.Map<FeedBackResponseDto>(feedback);
            mappedFeedback.UserName = currentUser.UserName;
            if (updatedFeedback )
                return new Result<FeedBackResponseDto>(mappedFeedback, 200, "Updated Successfullt");

            throw new ErrorResponseException(500, "Operation Failed", "Internal Server Error"); 



        }
    }
}
