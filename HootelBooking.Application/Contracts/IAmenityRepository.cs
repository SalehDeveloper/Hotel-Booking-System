using HootelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Contracts
{
    public interface IAmenityRepository:IBaseRepository<Amenity>
    {
        public Task<int> DeleteAsync (int  id);

        public Task<Amenity> GetByNameAsync( string name);  

        public Task<IEnumerable<Amenity>> GetActiveAsync();

        public Task<IEnumerable<Amenity>> GetInActiveAsync();


    }
}
