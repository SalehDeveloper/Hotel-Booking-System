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
    public class RoomStatusRepository : BaseRepository<RoomStatus>, IRoomStatusRepository
    {
        public RoomStatusRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<RoomStatus> GetByStatus(string status)
        {
            if (Enum.TryParse<enRoomStatus>(status , true , out enRoomStatus res))
            {
                return await _context.RoomStatuses.FirstOrDefaultAsync(x => x.Status == res);
            }
            return null;
        }
    }
}
