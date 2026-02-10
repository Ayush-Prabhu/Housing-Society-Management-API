
using HousingSociety.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HousingSociety.Infrastructure.Persistence.Configurations;

public class SocietyConfiguration : IEntityTypeConfiguration<Society>
{
    public void Configure(EntityTypeBuilder<Society> builder)
    {
        builder.ToTable("societies");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(300);
        builder.Property(x => x.City).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Pincode).HasMaxLength(20);
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("now() at time zone 'utc'");
        builder.HasIndex(x => new { x.Name, x.City });
    }
}
