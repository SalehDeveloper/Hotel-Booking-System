using HootelBooking.Domain.Enums;

namespace HootelBooking.Domain.Entities
{
    public class ReservationStatus
    {
        public int Id { get; set; }

        public enReservationStatus Staus { get; set; }

        //navigation properties 

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
