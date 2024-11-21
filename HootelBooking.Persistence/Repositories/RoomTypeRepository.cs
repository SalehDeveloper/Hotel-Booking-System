using HootelBooking.Application.Contracts;
using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using HootelBooking.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Persistence.Repositories
{
    public class RoomTypeRepository : BaseRepository<RoomType>, IRoomTypeRepository
    {
        public RoomTypeRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<RoomType> AddAsync(RoomType roomType)
        {
            if (roomType.Amenities != null && roomType.Amenities.Any())
            {
                // Ensure amenities are attached and tracked
                foreach (var amenity in roomType.Amenities)
                {
                    _context.Entry(amenity).State = EntityState.Unchanged; // Attach existing amenities
                }
            }

            await _context.RoomTypes.AddAsync(roomType);
            await _context.SaveChangesAsync(); // Automatically inserts RoomTypeAmenities

            return roomType;

        }
        public async Task<int> DeleteAsync(int id)
        {
            //0 : the RoomType is already deleted 
            //-1: RoomType not found 
            //id: RoomType with id Deleted successfully 
            var roomType = await _context.FindAsync<RoomType>(id);

            if (roomType is not null)
            {
                if (!roomType.IsActive)
                    return 0;

                roomType.IsActive = false;
                await _context.SaveChangesAsync();
                return id;
            }
            return -1;
        }

        public override async Task<RoomType> GetByIdAsync(int id)
        {
           return await _context.RoomTypes.Include(x => x.Amenities).FirstOrDefaultAsync(x=> x.Id == id);

            
            
        }

        public override async Task<IEnumerable<RoomType>> ListAllAsync()
        {
            var res = await _context.RoomTypes.Include(x => x.Amenities).ToListAsync();

            if (res.Any())
                return res; 
            return Enumerable.Empty<RoomType>();
        }

        public async Task<RoomType> GetByType(string type)
        {

            return await _context.RoomTypes.Include(x => x.Amenities).FirstOrDefaultAsync(x => x.Type == type);




        }

        public async  Task<IEnumerable<RoomType>> GetActiveAsync()
        {
            var res = await _context.RoomTypes.Include(x => x.Amenities).Where(x=> x.IsActive).ToListAsync();

            if (res.Any())
                return res;
            return Enumerable.Empty<RoomType>();
        }

        public async Task<IEnumerable<RoomType>> GetInActiveAsync()
        {

            var res = await _context.RoomTypes.Include(x => x.Amenities).Where(x => !x.IsActive).ToListAsync();

            if (res.Any())
                return res;
            return Enumerable.Empty<RoomType>();
        }

        public async Task<IEnumerable<Amenity>>GetAmenitiesByIds (List<int> ids)
        {
            return await _context.Amenities.Where(a => ids.Contains(a.ID)).ToListAsync();
        }
    }
}
