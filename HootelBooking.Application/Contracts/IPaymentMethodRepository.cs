using HootelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Contracts
{
    public interface IPaymentMethodRepository:IBaseRepository<PaymentMethod>
    {
        Task<PaymentMethod> GetByTypeAsync(string type);

        bool IsMethodAvailable(string type);

        Task<bool> IsMethodAvailable(int id);



    }
}
