namespace HootelBooking.Domain.Entities
{
    public class RoomTypeAmenity
    {
        public int RoomTypeID { get; set; }

        public int AmenityID { get; set; }

        //Navigation Properties 

        public RoomType RoomType { get; set; }

        public Amenity Amenity { get; set; }


    }
}
