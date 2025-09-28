using MediatR;

namespace VaccinationManagement.Application.Queries.GetByCpf
{
    public class GetByCpfRequest : IRequest<GetByCpfResponse>
    {
        public string Cpf { get; set; }
    }
}