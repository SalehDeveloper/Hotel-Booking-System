using HootelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Contracts
{
    public interface ICountryRepository : IBaseRepository<Country>  
    {
        public Task<Country> GetByName(string name);

        public Task<Country> GetByCode(string code);

        public Task<IEnumerable<Country>> GetActiveCountries();
        public Task<(IEnumerable<Country> , int ) > GetActiveCountriesPaginated( int pageNumber );

        public Task<IEnumerable<Country>> GetInActiveCountries();
        public Task<(IEnumerable<Country>, int)> GetInActiveCountriesPaginated(int pageNumber);

        public Task<IEnumerable<Country>> Search(string keyWord);
        
        Task<int> DeleteAsync(int id);
       
        Task<IEnumerable<State>> GetAllStatesInById(int id);

        Task<IEnumerable<State>> GetAllStatesInByName(string name);

        Task<Dictionary<Country , int >> GetCountriesPopulation();
        Task<KeyValuePair<Country , int >> GetCountriesPopulationById(int id);

         Task<bool> DoesCountryExist(string countryName);
        Task<bool> DoesStateBelongsToCountry(string state, string countryName);



    }
}
