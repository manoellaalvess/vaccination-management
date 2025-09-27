using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaccinationManagement.Domain.Entity;

namespace VaccinationManagement.Infrastructure.Configurations
{
    public class VaccineConfiguration : IEntityTypeConfiguration<Vaccine>
    {
        public void Configure(EntityTypeBuilder<Vaccine> builder)
        {
            builder.ToTable("Vaccines");
            
            builder.HasKey(v => v.VaccineId);

            builder.Property(v => v.VaccineName)
                   .HasMaxLength(150)
                   .IsRequired();

            // Relationship: Vaccine (1) -> (N) Vaccinations
            builder.HasMany(v => v.Vaccinations)
                   .WithOne(vac => vac.Vaccine)
                   .HasForeignKey(vac => vac.VaccineId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
