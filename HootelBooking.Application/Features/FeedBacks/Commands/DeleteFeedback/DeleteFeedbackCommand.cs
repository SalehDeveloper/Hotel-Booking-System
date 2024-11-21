using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.FeedBacks.Commands.DeleteFeedback
{
    public class DeleteFeedbackCommand:IRequest<Result<string>>
    {
        public int FeedbackId { get; set; } 
    }

}
