using VaccinationManagement.Domain.Entity;

namespace VaccinationManagement.Application.Queries.GetAllVaccine
{
    public class GetAllVaccineResponse
    {
        public List<Vaccine> Vaccines { get; set; }
    }
}