using HootelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Contracts
{
    public interface IReservationStatusRepository:IBaseRepository<ReservationStatus>
    {
        public Task<ReservationStatus> GetByStatus (string status);    
    }
}
