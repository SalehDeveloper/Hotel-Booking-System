using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.Country.Request
{
    public class CreateCountryRequestDto
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsActive { get; set; }
    }
}
