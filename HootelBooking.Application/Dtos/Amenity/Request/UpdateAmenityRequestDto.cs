using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.Amenity.Request
{
    public class UpdateAmenityRequestDto
    {
        public int ID { get; set; } 

        public string Name { get; set; }

        public string? Description { get; set; }

       // public string ModifiedBy { get; set; }

        public DateTime LastModifiedDate => DateTime.Now;


        public bool IsActive { get; set; }  


    }
}
