using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;


namespace HootelBooking.Persistence.Data.Config
{
    public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.ToTable("RoomTypes");

            builder.HasKey(roomType => roomType.Id);

            builder.Property(roomType => roomType.Id).ValueGeneratedOnAdd();


            builder
             .HasMany(rt => rt.Amenities)
             .WithMany(a => a.RoomTypes)
             .UsingEntity<RoomTypeAmenity>(
                 j => j
                     .HasOne(rta => rta.Amenity)
                     .WithMany(a => a.RoomTypeAmenity)
                     .HasForeignKey(rta => rta.AmenityID),
                 j => j
                     .HasOne(rta => rta.RoomType)
                     .WithMany(rt => rt.RoomTypeAmenity)
                     .HasForeignKey(rta => rta.RoomTypeID),
                 j =>
                 {
                     j.HasKey(rta => new { rta.RoomTypeID, rta.AmenityID });
                 });

            



          
        }
    }
}
