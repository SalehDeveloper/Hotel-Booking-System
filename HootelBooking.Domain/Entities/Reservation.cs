namespace HootelBooking.Domain.Entities
{
    public class Reservation : BaseEnity
    {
        public int Id { get; set; }

        public int RoomID { get; set; }

        public int UserID { get; set; }

        public DateTime BookDate { get; set; } = DateTime.Now;

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        // should be maximmum 4 
        public int NumberOfGuests { get; set; }

        //should be Maximmum 30 
        public int NumberOfNights { get; set; }

        public int ReservationStatusID { get; set; }
        public int RoomStatusID { get; set; }

        // in bussiness logic this will be equal to Room.Price *NumerOfNights + (25*NumberofGuests)
        public decimal TotalPrice { get; set; }
        

        //Navigation Properties 
        public ApplicationUser User { get; set; }

        public ReservationStatus ReversationStatus { get; set; }
        public Room Room { get; set; }

        public RoomStatus RoomStatus { get; set; }

        public Payment Payment { get; set; }

        public ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();



    }
}
