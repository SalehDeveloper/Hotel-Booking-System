

using Microsoft.EntityFrameworkCore;

namespace HootelBooking.Application.Extensions
{
    public static class QueryableExtension
    {
        public static async Task<IEnumerable<T>> ToPaginateListAsync<T>(this IQueryable<T> source, int pageNumber) where T : class
        {
            if (source == null) throw new Exception("Data is null!!");

            pageNumber = pageNumber==0 ? 1 : pageNumber;
            var pageSize = 10; 

            int count = await source.AsNoTracking().CountAsync();
            if (count == 0)
                Enumerable.Empty<T>();

            pageNumber = pageNumber<=0 ? 1 : pageNumber;
            
            var items = await source.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();

            return items;
            
        }
    }
}
