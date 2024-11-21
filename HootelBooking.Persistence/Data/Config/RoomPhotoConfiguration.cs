using HootelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Persistence.Data.Config
{
    public class RoomPhotoConfiguration : IEntityTypeConfiguration<RoomPhoto>
    {
        public void Configure(EntityTypeBuilder<RoomPhoto> builder)
        {
            builder.ToTable("RoomPhotos");

            builder.HasKey(roomPhoto => roomPhoto.PhotoId);

            builder.Property(roomPhoto => roomPhoto.PhotoId).ValueGeneratedOnAdd();

            builder.HasIndex(roomPhoto => roomPhoto.PhotoName).IsUnique();

           


        }
    }
}
