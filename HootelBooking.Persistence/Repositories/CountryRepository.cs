using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Extensions;
using HootelBooking.Domain.Entities;
using HootelBooking.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Persistence.Repositories
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<int> DeleteAsync(int id)
        {
            //0 : the Country is already deleted 
            //-1: country not found 
            //id: country with id Deleted successfully 
            var country = await _context.FindAsync<Country>(id);

            if (country is not null)
            {
                if (!country.IsActive)
                    return 0;

                country.IsActive = false;
                await _context.SaveChangesAsync();
                return id;
            }
            return -1;


        }

        public async Task<bool> DoesCountryExist(string countryName)
        {
            var country = await GetByName(countryName);

            return country != null && country.IsActive;
        }

        public async Task<bool> DoesStateBelongsToCountry(string state, string countryName)
        {
            if (await DoesCountryExist(countryName))
            {
                var allStates = await GetAllStatesInByName(countryName);
                return allStates.Any(x => x.Name.Equals(state, StringComparison.OrdinalIgnoreCase));
            }
            return false;
        }

        public async Task<IEnumerable<Country>> GetActiveCountries()
        {
            var result = await _context.Countries.AsNoTracking().ToListAsync();

            if (result.Any())
                return result;

            return Enumerable.Empty<Country>();



        }

        public async Task<(IEnumerable<Country>, int)> GetActiveCountriesPaginated( int pageNumber)
        {
            var total = await _context.Countries.Where(x => x.IsActive).CountAsync();


            var result = await _context.Countries.AsNoTracking().Where(country => country.IsActive).ToPaginateListAsync(pageNumber);

            if (total > 0)
                return (result, total);

            return (result, 0);

        }

        public async Task<IEnumerable<State>> GetAllStatesInById(int id)
        {
            var states = await _context.States.AsNoTracking().Where(x => x.CountryId == id).ToListAsync();

            if (states.Any())
                return states;
            return Enumerable.Empty<State>();
        }

        public async Task<IEnumerable<State>> GetAllStatesInByName(string name)
        {

            var states = await _context.States.Where(x => x.Country.Name == name).ToListAsync();

            if (states.Any())
                return states;

            return Enumerable.Empty<State>();


        }

        public async Task<Country> GetByCode(string code)
        {
            return await _context.Countries.AsNoTracking().FirstOrDefaultAsync(x => x.Code == code);


        }

        public async Task<Country> GetByName(string name)
        {

            return await _context.Countries.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);


        }

        public async Task<Dictionary<Country, int>> GetCountriesPopulation()
        {
            var countryPopulation = await _context.Countries
                                          .GroupJoin(
                                                     _context.Users,
                                                     country => country.Id,
                                                      user => user.CountryID,
                                                      (country, user) => new
                                                      {
                                                          Country = country,
                                                          Population = user.Count()
                                                      }
                                                     ).OrderByDescending(x => x.Population)
                                                      .ThenBy(result => result.Country.Name)
                                                      .ToDictionaryAsync(x => x.Country, x => x.Population);
            return countryPopulation;
        }

        public async Task<KeyValuePair<Country, int>> GetCountriesPopulationById(int id)
        {
            var result = await _context.Countries
                                    .Where(x => x.Id == id)
                                    .GroupJoin(_context.Users
                                                , country => country.Id
                                                , user => user.CountryID
                                                , (country, user) => new
                                                {
                                                    Country = country,
                                                    Population = user.Count()
                                                }).OrderByDescending(x => x.Population)
                                                .ThenBy(x => x.Country.Name)
                                                .FirstOrDefaultAsync();

            if (result != null)
            {
                return new KeyValuePair<Country, int>(result.Country, result.Population);
            }

            return default; // Handle case where no country is found
        }

        public async Task<IEnumerable<Country>> GetInActiveCountries()
            {
                var countries = await _context.Countries.Where(x => !x.IsActive).ToListAsync();

                if (countries.Any())
                    return countries;
                return Enumerable.Empty<Country>();
            }

        public async Task<(IEnumerable<Country>, int)> GetInActiveCountriesPaginated(int pageNumber)
            {

                var total = await _context.Countries.Where(x => !x.IsActive).CountAsync();


                var result = await _context.Countries.AsNoTracking().Where(country => !country.IsActive).ToPaginateListAsync(pageNumber);

                if (total > 0)
                    return (result, total);

                return (result, 0);
            }

      

        public async Task<IEnumerable<Country>> Search(string keyWord)
            {
                var countries = await _context.Countries.Where(x => x.Name.Contains(keyWord)).ToListAsync();

                if (countries.Any())
                    return countries;
                return Enumerable.Empty<Country>();
            }
    }

        
    
}
