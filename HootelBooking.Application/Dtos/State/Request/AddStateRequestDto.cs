using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.State.Request
{
    public class AddStateRequestDto
    { 
        public string Name { get; set; }    

        public bool IsActive { get; set;  }
        
        public int CountryId { get; set; }  


    }
}
