using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaccinationManagement.Domain.Entity;

namespace VaccinationManagement.Infrastructure.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("People");
            
            // Primary Key
            builder.HasKey(p => p.Cpf);

            builder.Property(p => p.Cpf)
                   .HasMaxLength(11) // CPF always with 11 digits without formatting
                   .IsRequired();

            builder.Property(p => p.Name)
                   .HasMaxLength(200)
                   .IsRequired();

            // Relationship: Person (1) -> (N) Vaccinations
            builder.HasMany(p => p.Vaccinations)
                   .WithOne(v => v.Person)
                   .HasForeignKey(v => v.PersonCpf)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
