using HootelBooking.Application.Services;
using HootelBooking.Persistence.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;


namespace HootelBooking.Persistence.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailConfiguration> _emailConfiguration;

        public EmailService(IOptions<EmailConfiguration> emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        public string GenerateEmailConfirmationMessageBody(string title, string code, string time)
        {
            return $@"
    <html>
        <body style='font-family: Arial, sans-serif; color: #333; line-height: 1.6;'>
            <p>Hello,</p>
            <p>Use the following code to <strong>{title}</strong>. This code is only valid for the next 
                <span style='font-weight: bold; color: #d9534f;'>{time} minutes</span>:</p>
            <div style='
                display: inline-block;
                padding: 15px 20px;
                margin: 15px 0;
                background-color: #f9f9f9;
                border: 1px solid #ddd;
                border-radius: 8px;
                box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
                font-size: 20px;
                color: #4CAF50; /* Green color for the code */
                font-weight: bold;
                text-align: center;'>
                {code}
            </div>
            <p>If you didn’t request this, you can safely ignore this email.</p>
            <p style='color:#000000; font-size: 16px;'>Best regards</p>
            <p style='color: #000; font-style: italic; font-weight: bold; font-size: 18px;'>Saleh Developer</p>
        </body>
    </html>";
        }
        public string GenerateEmailPaymentMessageBody(string userName , int reservationId ,  string roomNumber , DateTime checkIn , DateTime checkOut , int numberOfNights ,  decimal totalPrice  )
        {
            var message = $"Dear {userName}\n" +
                          $"Your Payent has been Comfirmed\n" +
                          $"ReservationId: {reservationId}\n" +
                          $"Room Number: {roomNumber}\n" +
                          $"CheckIn Date: {checkIn}\n" +
                          $"CheckOut Date: {checkIn}\n" +
                          $"Duration: {numberOfNights} days(s)\n" +
                          $"Total Price: {totalPrice.ToString("C")}\n" +
                          $":Saleh Developer\n";


            return message;
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("", _emailConfiguration.Value.From));

            emailMessage.To.AddRange(message.To);

            emailMessage.Subject = message.Subject;


            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };

            return emailMessage;
        }
        private void Send(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())

            {
                try
                {
                    client.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    client.Connect(_emailConfiguration.Value.SmtpServer, _emailConfiguration.Value.Port, true);

                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    client.Authenticate(_emailConfiguration.Value.UserName, _emailConfiguration.Value.Password);

                    client.Send(emailMessage);



                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex.Message}");
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();

                }
            }
        }

       
    }
}
