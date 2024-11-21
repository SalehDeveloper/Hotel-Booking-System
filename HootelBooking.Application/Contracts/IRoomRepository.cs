using HootelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Contracts
{
    public interface IRoomRepository:IBaseRepository<Room>
    {
        public Task<int> DeleteAsync(int roomId);
        public Task<IEnumerable<Room>> GetActiveAsync();
        public Task<IEnumerable<Room>> GetInActiveAsync();
        public Task<Room> GetByRoomNumber(string roomNumber);

        public Task<IEnumerable<Room>>GetByPrice (decimal price);   
        public Task<IEnumerable<Room>> GetByRoomType(string roomType);
  

        public Task<IEnumerable<Room>> Search(decimal? price, string? roomType,  string? viewType, string? BedType);
        Task<IEnumerable<string>> GetRoomPhotos(int roomId);






    }
}
