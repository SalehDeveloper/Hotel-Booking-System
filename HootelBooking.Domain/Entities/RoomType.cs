using HootelBooking.Domain.Enums;

namespace HootelBooking.Domain.Entities
{
    public class RoomType : BaseEnity
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string? Description { get; set; }


        //Navigation Properties 

        public ICollection<Room> Rooms { get; set; } = new List<Room>();
        public ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();

        public ICollection<RoomTypeAmenity> RoomTypeAmenity { get; set; } = new List<RoomTypeAmenity>();

    }
}
