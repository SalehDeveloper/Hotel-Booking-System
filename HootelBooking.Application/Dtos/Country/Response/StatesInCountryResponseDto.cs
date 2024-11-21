using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.Country.Response
{
    public class StatesInCountryResponseDto
    {
        public string CountryName { get; set; }

        public IEnumerable<StateCountryResponseDto> States {  get; set; } 

      
    }
}
