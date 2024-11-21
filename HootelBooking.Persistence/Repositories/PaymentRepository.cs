using HootelBooking.Application.Contracts;
using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using HootelBooking.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Persistence.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> ProcesspaymentAsync(int reservationId, string method)
        {  

            // you can use your own payment way (stripe , Paypal)
            var payment = await _context.Payments.Include(x => x.Reservation).ThenInclude(x => x.Room).FirstOrDefaultAsync(x => x.ReservationID == reservationId);

            payment.PaymentMethodID =  (int)Enum.Parse( typeof(enPaymentType), method );
            payment.PaidAt = DateTime.UtcNow;
            payment.Price = payment.Reservation.TotalPrice;
            payment.Reservation.ReservationStatusID = (int)enReservationStatus.CONFIRMED;
            payment.Reservation.Room.RoomStatusID = (int)enRoomStatus.RESERVED;

            await _context.SaveChangesAsync();
            return payment; 
           



        }

        public async Task<Payment>AddPayment (int reservationId , string method , decimal price )
        {
            var payment = new Payment()
            {
                ReservationID = reservationId,
                PaymentMethodID =  (int )Enum.Parse(typeof(enPaymentType), method) , 
                Price = price,
               PaidAt = DateTime.UtcNow,

            }; 

            await _context.Payments.AddAsync( payment );
            await _context.SaveChangesAsync();
            return payment;

        }
    }
}
