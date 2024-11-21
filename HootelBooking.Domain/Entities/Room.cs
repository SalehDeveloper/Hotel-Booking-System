using HootelBooking.Domain.Enums;

namespace HootelBooking.Domain.Entities
{
    public class Room : BaseEnity
    {

        public int RoomId { get; set; }

        public string RoomNumber { get; set; }  // UNIOUE

        public int RoomTypeID { get; set; }

        public int RoomStatusID { get; set; }


        public decimal Price { get; set; } // Price for only one night 

        public enBedType BedType { get; set; }

        public enViewType ViewType { get; set; }

        public bool IsActive { get; set; } = true; 
        //Navigation Properties 
        public RoomType RoomType { get; set; }

        public RoomStatus RoomStatus { get; set; }

        public ICollection<Reservation>? Reservations { get; set; } = new List<Reservation>();

        public ICollection<RoomPhoto>? RoomPhotos { get; set; } = new List<RoomPhoto>();



    }
}
