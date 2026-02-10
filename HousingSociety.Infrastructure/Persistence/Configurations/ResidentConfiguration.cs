
using HousingSociety.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HousingSociety.Infrastructure.Persistence.Configurations;

public class ResidentConfiguration : IEntityTypeConfiguration<Resident>
{
    public void Configure(EntityTypeBuilder<Resident> builder)
    {
        builder.ToTable("residents");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Phone).IsRequired().HasMaxLength(30);
        builder.Property(x => x.FlatNumber).IsRequired().HasMaxLength(50);
        builder.Property(x => x.JoinedOn).HasDefaultValueSql("now() at time zone 'utc'");

        builder.HasIndex(x => x.SocietyId);
        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasOne<Society>()
               .WithMany()
               .HasForeignKey(x => x.SocietyId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
