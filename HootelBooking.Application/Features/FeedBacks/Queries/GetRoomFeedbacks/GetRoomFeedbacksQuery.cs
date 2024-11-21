using HootelBooking.Application.Dtos.FeedBack.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.FeedBacks.Queries.GetRoomFeedbacks
{
    public class GetRoomFeedbacksQuery:IRequest<Result<List<FeedBackResponseDto>>>
    {
        public int RoomId { get; set; } 
    }
}
