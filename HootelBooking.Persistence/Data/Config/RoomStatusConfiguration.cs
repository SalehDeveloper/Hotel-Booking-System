using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HootelBooking.Persistence.Data.Config
{
    public class RoomStatusConfiguration : IEntityTypeConfiguration<RoomStatus>
    {
        public void Configure(EntityTypeBuilder<RoomStatus> builder)
        {
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID).ValueGeneratedOnAdd();

            builder.ToTable("RoomStatuses");


            builder.Property(x => x.Status)
                .HasConversion(
                x => x.ToString(),
                x => (enRoomStatus)Enum.Parse(typeof(enRoomStatus), x)
                );

        }
    }
}
