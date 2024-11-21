using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Domain.Entities
{

    public class Amenity : BaseEnity
    {

        public int ID { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }


        //Navigation Properties 

        public ICollection<RoomType> RoomTypes { get; set; } = new List<RoomType>();
        public ICollection<RoomTypeAmenity> RoomTypeAmenity { get; set; } = new List<RoomTypeAmenity>();
    }
}
