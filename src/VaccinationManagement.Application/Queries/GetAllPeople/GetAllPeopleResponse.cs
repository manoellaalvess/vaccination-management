using VaccinationManagement.Domain.Entity;

namespace VaccinationManagement.Application.Queries.GetAllPeople
{
    public class GetAllPeopleResponse
    {
        public List<Person> People { get; set; }
    }
}