using HootelBooking.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Persistence.Jobs
{
    public class ExpiredCodesJob
    {
        private readonly IAuthRepository _authRepository;

        public ExpiredCodesJob(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task ClearExpiredCodesAsync()
        {
            Console.WriteLine("ClearExpiredCodesAsync job is running");
            await _authRepository.ClearExpiredCodes();
        }
    }
}
