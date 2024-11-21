using HootelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HootelBooking.Persistence.Data.Config
{
    public class FeedBackConfiguration : IEntityTypeConfiguration<FeedBack>
    {
        public void Configure(EntityTypeBuilder<FeedBack> builder)
        {

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID).ValueGeneratedOnAdd();

            builder.ToTable("FeedBacks");

            builder.ToTable(t => t.HasCheckConstraint("CK_FeedBacks_Rate", "Rate >=1 AND Rate<=5"));
        }
    }
}
