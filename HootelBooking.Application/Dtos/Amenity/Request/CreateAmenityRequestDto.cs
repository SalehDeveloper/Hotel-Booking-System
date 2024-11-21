
namespace HootelBooking.Application.Dtos.Amenity.Request
{
    public class CreateAmenityRequestDto
    {
        public string Name { get; set; }
        
        public string? Description { get; set; }

        public bool IsActive {  get; set; } 

        //public string CreatedBy { get; set; }   
    }
}
