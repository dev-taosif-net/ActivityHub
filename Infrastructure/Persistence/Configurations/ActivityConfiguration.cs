using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ActivityConfiguration : BaseEntityConfiguration<Activity, string>
{
    public override void Configure(EntityTypeBuilder<Activity> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.Category).IsRequired().HasMaxLength(100);
        builder.Property(x => x.City).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Venue).IsRequired().HasMaxLength(200);

        builder.HasIndex(x => x.Date);
        builder.HasIndex(x => x.Category);
    }
}
