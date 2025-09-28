using VaccinationManagement.Domain.Entity;

namespace VaccinationManagement.Domain.Repository
{
    public interface IVaccineRepository
    {
        Task CreateVaccine(Vaccine vaccine);
        Task<List<Vaccine>> GetAllAsync();
        Task<Vaccine> GetByVaccineId(int vaccineId);
    }
}