

namespace HootelBooking.Application.Contracts
{
    public interface IBaseRepository<T>  where T : class
    {
         Task<T> GetByIdAsync(int id);

        Task<(IEnumerable<T>, int) > ListAllPaginatedAsync(int pageNumber);

        Task<IEnumerable<T>> ListAllAsync();

        Task<T> AddAsync(T entity);

        Task<bool> UpdatedAsync( T entity );

        Task AddRangeAsync(IEnumerable<T> entities);



    }
}
