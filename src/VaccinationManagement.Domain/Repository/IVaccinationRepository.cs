using VaccinationManagement.Domain.Entity;

namespace VaccinationManagement.Domain.Repository
{
    public interface IVaccinationRepository
    {
        Task AddVaccination(Vaccination vaccination);
        Task<Vaccination> GetByIdAsync(int vaccinationId);
        Task DeleteVaccinationAsync(int vaccinationId);
    }
}