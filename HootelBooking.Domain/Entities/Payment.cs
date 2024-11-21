namespace HootelBooking.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public int ReservationID { get; set; }

        public int PaymentMethodID { get; set; }

        
        // in bussiness logic this will be equals to price of the Reservation 
        public decimal Price { get; set; }
           

        public DateTime PaidAt { get; set; } = DateTime.Now;


        //Navigation Properties 
        public Reservation Reservation { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}
