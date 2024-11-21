using HootelBooking.Domain.Enums;

namespace HootelBooking.Domain.Entities
{
    public class PaymentMethod
    {
        public int ID { get; set; }

        public enPaymentType PaymentType { get; set; }

        //Navigation Properties 

        public bool IsActive { get; set; } = true;

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
