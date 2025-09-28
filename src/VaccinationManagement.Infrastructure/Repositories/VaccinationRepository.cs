using Microsoft.EntityFrameworkCore;
using VaccinationManagement.Domain.Entity;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.Infrastructure.Repositories
{
    public class VaccinationRepository : IVaccinationRepository
    {
        #region Properties

        private readonly VaccinationDbContext Context;

        #endregion

        #region Constructor

        public VaccinationRepository(VaccinationDbContext context)
        {
            Context = context;
        }

        #endregion

        #region Public Methods

        public async Task AddVaccination(Vaccination vaccination)
        {
            if (vaccination == null)
                throw new ArgumentNullException(nameof(vaccination));

            await Context.Vaccinations.AddAsync(vaccination);
            await Context.SaveChangesAsync();

            return;
        }
        
        public async Task<Vaccination> GetByIdAsync(int vaccinationId)
        {
            var vaccination = await Context.Vaccinations.FirstOrDefaultAsync(v => v.VaccinationId == vaccinationId);

            return vaccination;
        }

        public async Task DeleteVaccinationAsync(int vaccinationId)
        {
            var vaccination = await GetByIdAsync(vaccinationId);

            if (vaccination == null)
            {
                throw new ArgumentNullException(nameof(vaccination));
            }

            Context.Vaccinations.Remove(vaccination);
            await Context.SaveChangesAsync();

            return;
        }

        #endregion

    }
}