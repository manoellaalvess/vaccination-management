using VaccinationManagement.Domain.DTO;

namespace VaccinationManagement.Application.Queries.GetAllPeople
{
    public class GetAllPeopleResponse
    {
        public List<PersonDto> People { get; set; }
    }
}