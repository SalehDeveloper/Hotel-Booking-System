namespace HootelBooking.Domain.Enums
{
    public enum enRoomStatus
    {
        AVAILABLE = 1 ,   // Room is available for booking
        RESERVED =2 ,    // Room is reserved but not occupied
        OCCUPIED=3 ,    // Room is occupied
        UNDER_MAINTAINCE = 4  // Room is unavailable (e.g., under maintenance)
    }
}
