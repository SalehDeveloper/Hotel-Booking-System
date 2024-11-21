namespace HootelBooking.Domain.Entities
{
    public abstract class BaseEnity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
