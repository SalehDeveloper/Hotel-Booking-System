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
    public class AmenityRepository :  BaseRepository<Amenity>, IAmenityRepository
    {
        public AmenityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<int> DeleteAsync(int id)
        {

            //0 : the Amenity is already deleted 
            //-1: Amenity not found 
            //id: Amenity with id Deleted successfully 
            var amenity = await _context.FindAsync<Amenity>(id);

            if (amenity is not null)
            {
                if (!amenity.IsActive)
                    return 0;

                amenity.IsActive = false;
                await _context.SaveChangesAsync();
                return id;
            }
            return -1;
        }    

        public async Task<Amenity> GetByNameAsync(string name)
        {
            return await _context.Amenities.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<Amenity>> GetInActiveAsync()
        {
            var amenities=  await _context.Amenities.Where(x=>! x.IsActive).ToListAsync();

            if (amenities.Any())    
                return amenities;   

            return Enumerable.Empty<Amenity>();
        }

        public async Task<IEnumerable<Amenity>> GetActiveAsync()
        {
            var amenities = await _context.Amenities.Where(x => x.IsActive).ToListAsync();

            if (amenities.Any())
                return amenities;

            return Enumerable.Empty<Amenity>();
        }
    }
}
