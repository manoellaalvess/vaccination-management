using VaccinationManagement.Domain.Entity;

namespace VaccinationManagement.Domain.Repository
{
    public interface IPersonRepository
    {
        Task CreatePerson(Person person);
        Task DeletePerson(string cpf);
        Task<Person?> GetByCpfAsync(string cpf);
        Task<List<Person>> GetAllAsync();
    }
}