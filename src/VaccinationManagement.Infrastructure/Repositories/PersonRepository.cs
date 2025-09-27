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

        public Task AddAsync(Person person)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetByCpf(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}