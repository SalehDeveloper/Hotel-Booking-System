using HootelBooking.Application.Contracts;
using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using HootelBooking.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace HootelBooking.Persistence.Repositories
{
    public class RoomRepository :  BaseRepository<Room>,IRoomRepository
    {
        public RoomRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Room> GetByIdAsync(int id)
        {
            return await _context.Rooms.Include(x => x.RoomType).Include(x => x.RoomStatus).Include(x => x.RoomPhotos).FirstOrDefaultAsync(x => x.RoomId == id);
        }

        public override async Task<IEnumerable<Room>> ListAllAsync()
        {
            return await _context.Rooms.Include(x => x.RoomStatus).Include(x => x.RoomType).Include(x => x.RoomPhotos).ToListAsync();
        }

        public async Task<int> DeleteAsync(int roomId)
        {
            //0 : the Room is already deleted 
            //-1: Room not found 
            //id: Room with id Deleted successfully 
            var room = await _context.FindAsync<Room>(roomId);

            if (room is not null)
            {
                if (!room.IsActive)
                    return 0;

                room.IsActive = false;
                await _context.SaveChangesAsync();
                return roomId;
            }
            return -1;
        }

        public async Task<IEnumerable<Room>> GetActiveAsync()
        {
          var result  = await _context.Rooms.Include(x=>x.RoomStatus).Include(x=>x.RoomType).Include(x=> x.RoomPhotos).Where(x=> x.IsActive).ToListAsync();   

            if (result.Any())
                return result;  
            return Enumerable.Empty<Room>();
        }

        
      

        public async Task<IEnumerable<Room>> GetByPrice(decimal price)
        {
            var res= await _context.Rooms.Include(x=>x.RoomType).Include(x=>x.RoomStatus).Include(x => x.RoomPhotos).Where(x=> x.Price == price).ToListAsync();

            if (res.Any())
                return res;
            return Enumerable.Empty<Room>();
        }

        public async Task<Room> GetByRoomNumber(string roomNumber)
        {
            var res = await _context.Rooms.Include(x => x.RoomStatus).Include(x => x.RoomType).Include(x => x.RoomPhotos).FirstOrDefaultAsync(x => x.RoomNumber == roomNumber);

            return res;
        }

        public async Task<IEnumerable<Room>> GetByRoomType(string roomType)
        {
            var res = await _context.Rooms.Include(x => x.RoomStatus).Include(x=>x.RoomType).Include(x => x.RoomPhotos).Where(x=>x.RoomType.Type == roomType).ToListAsync();

            if (res.Any()) return res;

            return Enumerable.Empty<Room>();
        }

        public async Task<IEnumerable<Room>> GetInActiveAsync()
        {
            var res = await _context.Rooms.Include(x=>x.RoomStatus).Include(x=>x.RoomType).Include(x => x.RoomPhotos).Where(x=>!x.IsActive).ToListAsync(); 

            if (res.Any())
                return res; 
            return Enumerable.Empty<Room>();

        }

        public async Task<IEnumerable<Room>> Search(decimal? price, string? roomType, string? viewType, string? bedType)
        {
            
            var query = _context.Rooms.AsQueryable();

            
            if (price.HasValue)
            {
                query = query.Where(x => x.Price == price.Value);
            }

            if (!string.IsNullOrWhiteSpace(roomType))
            {
                query = query.Where(x => x.RoomType.Type == roomType.Trim());
            }

            if (!string.IsNullOrWhiteSpace(viewType))
            {
                if (Enum.TryParse<enViewType>(viewType.Trim(), true, out var enViewTypeValue))
                {
                    query = query.Where(x => x.ViewType == enViewTypeValue);
                }
                
            }

            if (!string.IsNullOrWhiteSpace(bedType))
            {
                if (Enum.TryParse<enBedType>(bedType.Trim(), true, out var enBedTypeValue))
                {
                    query = query.Where(x => x.BedType == enBedTypeValue);
                }
               
            }

            
            query = query.Include(x => x.RoomStatus).Include(x => x.RoomPhotos).Include(x => x.RoomType);

            
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<string>> GetRoomPhotos (int roomId)
        {
            var photos = await _context.RoomPhotos.Where(x=>x.RoomId == roomId).Select(x=> x.PhotoName).ToListAsync(); 
        
          if (photos.Any()) 
                return photos;
          return Enumerable.Empty<string>();
        
        }


        



    }
}
