using HootelBooking.Application.Dtos.FeedBack.Request;
using HootelBooking.Application.Dtos.FeedBack.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.FeedBacks.Commands.AddFeedback
{
    public class AddFeedbackCommand : IRequest<Result<FeedBackResponseDto>>
    {
        public AddFeedbackRequestDto  Request{  get; set; }
    }
}
