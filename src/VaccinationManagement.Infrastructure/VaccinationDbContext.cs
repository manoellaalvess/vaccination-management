using Microsoft.EntityFrameworkCore;
using VaccinationManagement.Domain.Entity;
using VaccinationManagement.Infrastructure.Configurations;

namespace VaccinationManagement.Infrastructure
{
    public class VaccinationDbContext : DbContext
    {
        public VaccinationDbContext(DbContextOptions<VaccinationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Vaccination> Vaccinations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new VaccineConfiguration());
            modelBuilder.ApplyConfiguration(new VaccinationConfiguration());
        }
    }
}
