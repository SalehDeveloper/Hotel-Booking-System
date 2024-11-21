using HootelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Contracts
{
    public interface IReservationRepository
    {


        Task<bool> IsRoomAvailableAsync (int roomId , DateTime checkInDate, DateTime checkOutDate);
        Task<Reservation> CreateReservationAsync(string roomNumber, int userId, DateTime checkInDate, DateTime checkOutDate, int numberOfGuests);
       
        Task<bool> CancelReservationAsync(int reservationId);
        Task<Reservation> CheckInAsync(int reservationId);
        Task<Reservation> CheckOutAsync(int reservationId);
        Task<bool> IsReservationExistAsync(int reservationId);
        Task<Reservation> GetReservationByIdAsync(int reservationId);
        Task<List<Reservation>> GetExpiredReservationsAsync();
        Task<List<Reservation>> GetAllReservationsAsync();
        Task<List<Reservation>> GetActiveReservationsAsync();


    }
}
