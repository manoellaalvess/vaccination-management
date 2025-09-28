using VaccinationManagement.Domain.DTO;

namespace VaccinationManagement.Application.Queries.GetAllPeople
{
    public class GetAllPeopleResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<PersonDto> People { get; set; }
    }
}