namespace HootelBooking.Domain.Entities
{
    public class FeedBack
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int ReservationID { get; set; }

        public int Rate { get; set; }  // should be between 1 -  5    

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //Navigation Properties 

        public ApplicationUser User { get; set; }

        public Reservation Reservation { get; set; }
    }
}
