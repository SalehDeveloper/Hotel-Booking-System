using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.FeedBacks.Commands.DeleteFeedback
{
    public class DeleteFeedbackCommandHandler : IRequestHandler<DeleteFeedbackCommand, Result<string>>
    {
        private readonly IFeedBackRepository _feedBackRepository;
        private readonly IUserRepository _userRepository;

        public DeleteFeedbackCommandHandler(IFeedBackRepository feedBackRepository, IUserRepository userRepository)
        {
            _feedBackRepository = feedBackRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<string>> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
        {
            var feedback = await _feedBackRepository.GetByIdAsync(request.FeedbackId);

            if (feedback == null)
                return new Result<string>(404, "Feedback Not Found");

            var currentUser  = await _userRepository.GetCurrentUserAsync();

            var isFeedbackForUser = currentUser.Id == feedback.UserID; 
            if (!isFeedbackForUser)
                return new Result<string>(403, "You cannot Delete this FeedBack");

          var isDeleted =  await _feedBackRepository.DeleteFeedBackAsync(feedback.ID);

            if (isDeleted)
                return new Result<string>($"FeedBack With ID: {feedback.ID} has been Deleted " ,200 , "Dont Successfully");

            throw new ErrorResponseException(500, "Operation Failed", "Internal Server Error");






        }
    }
}
