using HootelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Contracts
{
    public interface IStateRepository:IBaseRepository<State>
    {
        public Task<int> DeleteAsync (int id);

        public Task<State> GetByNameAsync (string name);

       

        public Task< (IEnumerable<State>,  int ) > GetActiveStatesPaginatedAsync(int pageNumber);
        public Task<(IEnumerable<State>, int)> GetInActiveStatesPaginatedAsync(int pageNumber);

        public Task<IEnumerable<State>> SearchAsync (string keyword);




    }
}
