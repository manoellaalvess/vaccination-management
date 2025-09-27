using Microsoft.EntityFrameworkCore;
using VaccinationManagement.Domain.Entity;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.Infrastructure.Repositories
{
    public class VaccineRepository : IVaccineRepository
    {
        #region Properties

        private readonly VaccinationDbContext Context;

        #endregion

        #region Constructor

        public VaccineRepository(VaccinationDbContext context)
        {
            Context = context;
        }

        #endregion

        #region Public Methods

        public Task CreateVaccine(Vaccine vaccine)
        {
            if (string.IsNullOrWhiteSpace(vaccine.VaccineName))
                throw new ArgumentNullException(nameof(vaccine.VaccineName));

            Context.Vaccines.Add(vaccine);
            return Context.SaveChangesAsync();
        }

        public async Task<List<Vaccine>> GetAllAsync()
        {
            return await Context.Vaccines.ToListAsync();
        }

        #endregion
    }
}