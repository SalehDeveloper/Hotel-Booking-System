using HootelBooking.Domain.Entities;
using HootelBooking.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Services
{
    public class MessageService
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageService(IEmailService emailService, UserManager<ApplicationUser> userManager)
        {
            _emailService = emailService;
            _userManager = userManager;
        }

        public async Task SendMessage( ApplicationUser user  , string code ,  string title  , string subject , string duration )
        {
          
            var messageBody = _emailService.GenerateEmailConfirmationMessageBody(subject, code,duration);

            // Send confirmation email using the email service
            var message = new Message(new string[] { user.Email }, title, messageBody);

            _emailService.SendEmail(message);
        }


        public async Task SendMessage( string title, ApplicationUser user , int reservationId , string roomNumber, DateTime checkIn , DateTime checkOut , int duration , decimal totalPrice)
        {

            var messageBody = _emailService.GenerateEmailPaymentMessageBody( user.UserName , reservationId , roomNumber , checkIn , checkOut , duration ,totalPrice);

            // Send confirmation email using the email service
            var message = new Message(new string[] { user.Email }, title, messageBody);

            _emailService.SendEmail(message);
        }
    }
}
