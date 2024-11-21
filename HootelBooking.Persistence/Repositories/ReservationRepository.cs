using Hangfire;
using HootelBooking.Application.Contracts;
using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using HootelBooking.Persistence.Data;
using HootelBooking.Persistence.Jobs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Persistence.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {

            bool  isRoomAvailable =! await _context.Reservations
             .AnyAsync(r => r.RoomID == roomId &&
                       ((r.CheckInDate < checkOutDate) && (checkInDate < r.CheckOutDate)) &&
                       r.ReservationStatusID != (int)enReservationStatus.CANCELED);

            return isRoomAvailable;




        }
        public async Task<bool> IsReservationExistAsync(int reservationId)
        {
            return await  _context.Reservations.AnyAsync(x=> x.Id == reservationId);
        }
        public async Task<Reservation> CreateReservationAsync(string roomNumber, int userId, DateTime checkInDate, DateTime checkOutDate, int numberOfGuests)
        {
            var room = await _context.Rooms.Include(x => x.Reservations).FirstOrDefaultAsync(x => x.RoomNumber == roomNumber);
           
            
            int numberOfDays = (checkOutDate - checkInDate).Days;

            decimal totalPrice = GetTotalPrice(room.Price, numberOfDays, numberOfGuests);

           var reservation = new Reservation()
           {
                RoomID = room.RoomId,
                UserID = userId,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                NumberOfGuests = numberOfGuests,
                NumberOfNights = numberOfDays,
                ReservationStatusID = (int)enReservationStatus.PENDING,
                RoomStatusID = (int)enRoomStatus.AVAILABLE , 
                TotalPrice = totalPrice , 
                CreatedBy = "system" , 
                CreatedAt = DateTime.UtcNow 
        };


         _context.Reservations.Add(reservation);
         room.RoomStatusID = (int)enRoomStatus.RESERVED;
         await  _context.SaveChangesAsync();

            return reservation;

        }
        public async Task<bool> CancelReservationAsync(int reservationId)
        {
            var reservation =await _context.Reservations.Include(x => x.Room).FirstOrDefaultAsync(x => x.Id == reservationId);

            reservation.ReservationStatusID = (int)enReservationStatus.CANCELED;
            reservation.Room.RoomStatusID = (int)enRoomStatus.AVAILABLE;
            reservation.IsActive = false;

            await _context.SaveChangesAsync();    
            return true;


        }
        public async Task<Reservation> CheckInAsync(int reservationId)
        {
            var reservation  = await _context.Reservations.Include(x=> x.Room).FirstOrDefaultAsync(x=> x.Id == reservationId);

            reservation.ReservationStatusID = (int)enReservationStatus.CHECKEDIN;
            reservation.Room.RoomStatusID = (int)  enRoomStatus.OCCUPIED;

            await _context.SaveChangesAsync();

            return reservation;
        }
        public async Task<Reservation> CheckOutAsync(int reservationId)
        {
            var reservation = await _context.Reservations.Include(x => x.Room).FirstOrDefaultAsync(x => x.Id == reservationId);

            reservation.ReservationStatusID = (int)enReservationStatus.COMPLETED;
            reservation.Room.RoomStatusID = (int)enRoomStatus.AVAILABLE;
            reservation.IsActive = false;
            await _context.SaveChangesAsync();

            return reservation;

        }
        public async Task<Reservation> GetReservationByIdAsync (int reservationId)
        {
            return await _context.Reservations.Include(x => x.Room).FirstOrDefaultAsync(x=> x.Id ==reservationId);
        }
        public async Task<List<Reservation>> GetExpiredReservationsAsync()
        {
            
        
        var expirationTime = DateTime.Now.AddHours(-24); // Reservations older than 24 hours

            var expiredReservations = await _context.Reservations.Include(x => x.Room)
                .Where(r => r.ReservationStatusID == (int)enReservationStatus.PENDING && 
                            r.BookDate <= expirationTime &&  
                            r.ReservationStatusID != (int)enReservationStatus.CANCELED)
                .ToListAsync();

            return expiredReservations;
        }
        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            return await _context.Reservations.Include(x=>x.Room).ToListAsync();
        }
        public async Task<List<Reservation>> GetActiveReservationsAsync()
        {
            return await _context.Reservations.Include(x => x.Room).Where(x=> x.IsActive).ToListAsync();
        }
        private decimal GetTotalPrice( decimal roomPrice ,  int numberofDays , int numberOfGuests)
        {
            //Room.Price *numberofDays + (25*NumberofGuests)
            return roomPrice * numberofDays + (25 * numberOfGuests);
        }





      
    }
}
