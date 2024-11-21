using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;


namespace HootelBooking.Persistence.Data.Config
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");

            builder.HasKey(room => room.RoomId);

            builder.Property(room => room.RoomId).ValueGeneratedOnAdd();

            builder.HasIndex(room => room.RoomNumber).IsUnique();

            builder.Property(x => x.BedType)
                  .HasConversion(
                  x => x.ToString(),
                  x => (enBedType)Enum.Parse(typeof(enBedType), x)
                  );

            builder.Property(x => x.ViewType)
                  .HasConversion(
                  x => x.ToString(),
                  x => (enViewType)Enum.Parse(typeof(enViewType), x)
            );


            builder.Property(p => p.Price)
            .HasPrecision(18, 2); //

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);
            //relation with RoomStatuses

            builder.HasOne(room => room.RoomStatus)
                   .WithMany(status => status.Rooms)
                   .HasForeignKey(room => room.RoomStatusID)
                   .IsRequired();

            // relation with RoomTypes 
            builder.HasOne(room => room.RoomType)
                    .WithMany(type => type.Rooms)
                    .HasForeignKey(room => room.RoomTypeID)
                    .IsRequired();

            //relation with RoomPhotos  
            builder.HasMany(r => r.RoomPhotos)
           .WithOne(p => p.Room)
           .HasForeignKey(p => p.RoomId)
           .IsRequired()
           .OnDelete(DeleteBehavior.Cascade);





        }
    }
}
