using MediatR;

namespace VaccinationManagement.Application.Command.DeletePerson
{
    public class DeletePersonRequest : IRequest<DeletePersonResponse>
    {
        public string Cpf { get; set; }
    }
}