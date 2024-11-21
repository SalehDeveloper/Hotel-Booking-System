using HootelBooking.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Services
{
    public interface IEmailService
    {
        public void SendEmail(Message message);
        public string GenerateEmailConfirmationMessageBody(string title, string code, string time);

        public string GenerateEmailPaymentMessageBody(string userName, int reservationId, string roomNumber, DateTime checkIn, DateTime checkOut, int numberOfNights, decimal totalPrice);
    }
}
