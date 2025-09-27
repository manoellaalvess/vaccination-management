using VaccinationManagement.Domain.Entity;

namespace VaccinationManagement.Application.Command.CreatePerson
{
    public class CreatePersonAdapter
    {
        public static Person BuildToPerson(CreatePersonRequest request)
        {
            return new Person(request.Cpf, request.Name);
        }
    }
}