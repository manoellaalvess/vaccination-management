using VaccinationManagement.Domain.DTO;

namespace VaccinationManagement.Application.Queries.GetAllVaccine
{
    public class GetAllVaccineResponse
    {
        public List<VaccineDto> Vaccines { get; set; }
    }
}