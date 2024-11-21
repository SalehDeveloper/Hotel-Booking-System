using HootelBooking.Application.Contracts;
using HootelBooking.Application.Extensions;
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
    public class StateRepository : BaseRepository<State>, IStateRepository
    {
        public StateRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<int> DeleteAsync(int id)
        {

            //0 : the State is already deleted 
            //-1: State not found 
            //id: State with id Deleted successfully 
            var state = await _context.FindAsync<State>(id);

            if (state is not null)
            {
                if (!state.IsActive)
                    return 0;

                state.IsActive = false;
                await _context.SaveChangesAsync();
                return id;
            }
            return -1;
        }

        public async Task<(IEnumerable<State>, int)> GetActiveStatesPaginatedAsync(int pageNumber)
        {

            var total = await _context.States.Where(x => x.IsActive).CountAsync();


            var result = await _context.States.Include(x=> x.Country).AsNoTracking().Where(state => state.IsActive).ToPaginateListAsync(pageNumber);

            if (total > 0)
                return (result, total);

            return (result, 0);

        }

       
        public async Task<State> GetByNameAsync(string name)
        {
            return await _context.States.Include(x=>x.Country).AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<(IEnumerable<State>, int)> GetInActiveStatesPaginatedAsync(int pageNumber)
        {
            var total = await _context.States.Where(x => !x.IsActive).CountAsync();


            var result = await _context.States.Include(x => x.Country).AsNoTracking().Where(country => !country.IsActive).ToPaginateListAsync(pageNumber);

            if (total > 0)
                return (result, total);

            return (result, 0);
        }

        public async Task<IEnumerable<State>> SearchAsync(string keyword)
        {
            var states = await _context.States.Include(x => x.Country).Where(x => x.Name.Contains(keyword)).ToListAsync();

            if (states.Any())
                return states;
            return Enumerable.Empty<State>();

        }
    }
}
