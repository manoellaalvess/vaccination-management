using VaccinationManagement.Domain.DTO;

namespace VaccinationManagement.Application.Queries.GetVaccinationCard
{
    public class GetVaccinationCardResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public PersonDto Person { get; set; }
    }
}