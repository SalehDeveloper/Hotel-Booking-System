using HootelBooking.Application.Contracts;
using HootelBooking.Domain.Entities;
using HootelBooking.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Persistence.Repositories
{
    public class FeedBackRepository : BaseRepository<FeedBack>, IFeedBackRepository
    {
        public FeedBackRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> DeleteFeedBackAsync(int feedBackID)
        {   
            var feedback  = await _context.FeedBacks.FirstOrDefaultAsync(x => x.ID == feedBackID);
           
            _context.Remove(feedback);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<FeedBack>> GetRoomFeedBacks(int roomID)
        {

            var feedbacks =await _context.FeedBacks.Include(x => x.Reservation).ThenInclude(x => x.Room).Where(x => x.Reservation.RoomID == roomID).ToListAsync();

            return feedbacks;


           
        }
    }
}
