using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.State.Response
{
    public  class StateResponseDto
    { 
        public int Id { get; set; }  

        public string Name { get; set; }    

        public bool IsActive { get; set; }

        public string Country { get; set; }

    }
}
