using HootelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Contracts
{
    public interface IFeedBackRepository:IBaseRepository<FeedBack>
    {
        Task<bool> DeleteFeedBackAsync(int feedBackID);

        Task<List<FeedBack>> GetRoomFeedBacks (int roomID);


    }
}
