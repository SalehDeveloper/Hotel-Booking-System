using Microsoft.AspNetCore.Identity;

namespace HootelBooking.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLogin { get; set; }

        public bool IsActive { get; set; } = false;

        public string CreatedBy { get; set; }


        public string? ModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public int CountryID { get; set; }

        public int StateID { get; set; }

         public string? EmailConfirmationCode { get; set; }
        public DateTime? ConfirmationCodeExpiry { get; set; }

        public string? ResetPasswordCode { get; set; }
        public DateTime? ResetPasswordCodeExpiry { get; set; }

        public string? TwoFactorCode { get; set; }
        public DateTime? TwoFactorCodeExpiry { get; set; }

        public string PhotoName { get; set; } 

        public Country Country { get; set; }

        public State State { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        public ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();
    }
}
