using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Contracts
{
    public interface IRoomTypeRepository:IBaseRepository<RoomType>
    {
        public Task<int> DeleteAsync (int id);

        public Task<RoomType> GetByType(string type);

        public Task<IEnumerable<RoomType>> GetActiveAsync();

        public Task<IEnumerable<RoomType>> GetInActiveAsync();

        public Task<IEnumerable<Amenity>> GetAmenitiesByIds(List<int> ids);






    }
}
