using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.State.Request
{
    public class UpdateStateRequestDto
    {
        public int Id { get; set; } 

        public string Name { get; set; }
        
        public bool IsActive {  get; set; }   

        public int CountryId { get; set; }  


    }
}
