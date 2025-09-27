using VaccinationManagement.Domain.Entity;

namespace VaccinationManagement.Domain.Repository
{
    public interface IPersonRepository
    {
        Task CreatePerson(Person person);
        Task<Person> GetByCpf(string cpf);
        Task AddAsync(Person person);
        Task DeleteAsync(string cpf);
        Task SaveChangesAsync();
    }
}