using MediatR;

namespace VaccinationManagement.Application.Command.CreatePerson
{
    public class CreatePersonRequest : IRequest<CreatePersonResponse>
    {
        public string Cpf { get; set; }
        public string Name { get; set; }
    }
}