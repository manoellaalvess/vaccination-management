using VaccinationManagement.Domain.DTO;

namespace VaccinationManagement.Application.Queries.GetByCpf
{
    public class GetByCpfResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public PersonDto Person { get; set; }
    }
}