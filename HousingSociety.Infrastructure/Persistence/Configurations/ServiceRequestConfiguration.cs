
using HousingSociety.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HousingSociety.Infrastructure.Persistence.Configurations;

public class ServiceRequestConfiguration : IEntityTypeConfiguration<ServiceRequest>
{
    public void Configure(EntityTypeBuilder<ServiceRequest> builder)
    {
        builder.ToTable("service_requests");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type).HasConversion<int>().IsRequired();
        builder.Property(x => x.Status).HasConversion<int>().IsRequired();

        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Description).HasMaxLength(2000);

        builder.Property(x => x.CreatedAt).HasDefaultValueSql("now() at time zone 'utc'");

        builder.HasIndex(x => x.ResidentId);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.Type);

        builder.HasOne<Resident>()
               .WithMany()
               .HasForeignKey(x => x.ResidentId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
