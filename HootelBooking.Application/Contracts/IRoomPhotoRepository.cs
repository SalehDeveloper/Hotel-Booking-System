using HootelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Contracts
{
    public interface IRoomPhotoRepository:IBaseRepository<RoomPhoto>
    {
        public Task<bool> DeleteAsync(List<string> photoNames);
    }
}
