using HootelBooking.Application.Contracts;
using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using HootelBooking.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Persistence.Repositories
{
    public class ReservationStatusRepository : BaseRepository<ReservationStatus>, IReservationStatusRepository
    {
        public ReservationStatusRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ReservationStatus> GetByStatus(string status)
        {
           if ( Enum.TryParse<enReservationStatus>(status , true , out enReservationStatus res))
            {
                return await _context.ReservationStatuses.FirstOrDefaultAsync(x => x.Staus == res);
            }
            return null;
        }
    }
}
