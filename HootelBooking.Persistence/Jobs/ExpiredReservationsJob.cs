using HootelBooking.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Persistence.Jobs
{
    public class ExpiredReservationsJob
    {
        private readonly IReservationRepository _reservationRepository;

        public ExpiredReservationsJob(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task ClearExpiredReservationsAsync()
        {
            var expiredReservations= await _reservationRepository.GetExpiredReservationsAsync();    

            foreach (var reservation in expiredReservations)
            {
                await _reservationRepository.CancelReservationAsync(reservation.Id);
            }
        }


    }
}
