using HootelBooking.Application.Dtos.FeedBack.Request;
using HootelBooking.Application.Dtos.FeedBack.Response;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.FeedBacks.Commands.UpdateFeedback
{
    public class UpdateFeedbackCommand:IRequest<Result<FeedBackResponseDto>>
    {
        public UpdateFeedbackRequestDto Request { get; set; }   


    }
}
