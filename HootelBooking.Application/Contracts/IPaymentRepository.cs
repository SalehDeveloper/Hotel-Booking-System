using HootelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Contracts
{
    public interface IPaymentRepository
    {

        Task<Payment> ProcesspaymentAsync(int reservationId, string method);
        Task<Payment> AddPayment(int reservationId, string method, decimal price);


    }
}
