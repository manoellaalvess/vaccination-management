using Microsoft.EntityFrameworkCore;
using VaccinationManagement.Domain.Entity;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        #region Properties

        private readonly VaccinationDbContext Context;

        #endregion

        #region Constructor

        public PersonRepository(VaccinationDbContext context)
        {
            Context = context;
        }

        #endregion

        #region Public Methods

        public async Task CreatePerson(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));

            await Context.People.AddAsync(person);
            await Context.SaveChangesAsync();

            return;
        }

        public async Task DeletePerson(string cpf)
        {
            var person = await GetByCpfAsync(cpf);
            if (person != null)
            {
                Context.People.Remove(person);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<Person?> GetByCpfAsync(string cpf)
        {
            return await Context.People
                .Include(p => p.Vaccinations).FirstOrDefaultAsync(p => p.Cpf == cpf);
        }

        public async Task<List<Person>> GetAllAsync()
        {
            return await Context.People
                .Include(p => p.Vaccinations)
                .ToListAsync();
        }

        #endregion
    }
}