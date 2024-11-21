using HootelBooking.Domain.Entities;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Contracts
{
    public interface IRoomStatusRepository:IBaseRepository<RoomStatus>
    { 
       public Task<RoomStatus> GetByStatus (string status); 
    }
}
