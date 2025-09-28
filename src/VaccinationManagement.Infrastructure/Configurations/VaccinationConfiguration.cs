using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaccinationManagement.Domain.Entity;

namespace VaccinationManagement.Infrastructure.Configurations
{
    public class VaccinationConfiguration : IEntityTypeConfiguration<Vaccination>
    {
        public void Configure(EntityTypeBuilder<Vaccination> builder)
        {
            builder.ToTable("Vaccinations");

            builder.HasKey(v => v.VaccinationId);

            builder.Property(v => v.PersonCpf)
                   .HasMaxLength(11)
                   .IsRequired();

            builder.Property(v => v.VaccineId)
                   .IsRequired();

            builder.Property(v => v.VaccinationDate)
                   .IsRequired();

            builder.Property(v => v.Dose)
                   .IsRequired();

            // Relationship: Vaccination -> Person (N:1)
            builder.HasOne(v => v.Person)
                .WithMany(p => p.Vaccinations)
                .HasForeignKey(v => v.PersonCpf)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Vaccination -> Vaccine (N:1)
            builder.HasOne(v => v.Vaccine)
                .WithMany(vac => vac.Vaccinations)
                .HasForeignKey(v => v.VaccineId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
