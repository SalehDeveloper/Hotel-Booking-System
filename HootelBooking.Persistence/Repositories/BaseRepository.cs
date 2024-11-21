using HootelBooking.Application.Contracts;
using HootelBooking.Application.Extensions;
using HootelBooking.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async  virtual Task<T> AddAsync(T entity)
        {

             await _context.Set<T>().AddAsync(entity);
             await _context.SaveChangesAsync(); 
             return entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
        public async virtual Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async virtual  Task<IEnumerable<T>> ListAllAsync()
        {
            var res =await _context.Set<T>().ToListAsync();

            if (res.Any())
                return res;  

            return Enumerable.Empty<T>();
        }

        public async Task<(IEnumerable<T> , int )> ListAllPaginatedAsync( int pageNumber )
        {
            var total = await _context.Set<T>().CountAsync();
            var countries =  await _context.Set<T>().ToPaginateListAsync(pageNumber );
            

            if (countries.Any())
                return (countries , total) ;
            return (countries , 0);
        }   

        public async Task<bool> UpdatedAsync(T entity)
        {

            _context.Set<T>().Update(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
    
}
