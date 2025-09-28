using VaccinationManagement.Domain.DTO;

namespace VaccinationManagement.Application.Queries.GetAllVaccine
{
    public class GetAllVaccineResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<VaccineDto> Vaccines { get; set; }
    }
}