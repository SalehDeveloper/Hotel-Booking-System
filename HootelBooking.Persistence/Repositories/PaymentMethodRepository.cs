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
    public class PaymentMethodRepository : BaseRepository<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PaymentMethod> GetByTypeAsync(string type)
        {
            if (Enum.TryParse<enPaymentType>(type  ,true , out enPaymentType res))
            {
                return await _context.PaymentMethods.FirstOrDefaultAsync(x => x.PaymentType == res);
            }
            return null; 
        }

        public bool IsMethodAvailable(string type)
        {

            if (Enum.TryParse<enPaymentType>(type, true, out var _))
            {
                return true; 
            }
            return false;
        }

        public async Task<bool> IsMethodAvailable(int id)
        {
            var res = await _context.PaymentMethods.FindAsync(id);

            if (res is not null)
                return true;
            return false;
        }
    }
}
