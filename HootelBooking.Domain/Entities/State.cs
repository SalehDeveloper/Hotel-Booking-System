namespace HootelBooking.Domain.Entities
{
    public class State
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public int CountryId { get; set; }

        public bool IsActive { get; set; } = true;
        public Country Country { get; set; } 

        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

    }
}
