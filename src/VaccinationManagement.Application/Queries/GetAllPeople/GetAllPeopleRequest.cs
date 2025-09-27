using MediatR;

namespace VaccinationManagement.Application.Queries.GetAllPeople
{
    public class GetAllPeopleRequest : IRequest<GetAllPeopleResponse>
    {
        public string Trigger { get; set; } = "trigger";
    }
}