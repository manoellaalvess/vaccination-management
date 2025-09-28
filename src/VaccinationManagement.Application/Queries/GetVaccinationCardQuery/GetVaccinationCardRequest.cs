using MediatR;

namespace VaccinationManagement.Application.Queries.GetVaccinationCard
{
    public class GetVaccinationCardRequest : IRequest<GetVaccinationCardResponse>
    {
        public string Cpf { get; set; }
    }
}