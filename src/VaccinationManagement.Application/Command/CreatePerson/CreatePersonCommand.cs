using MediatR;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.Application.Command.CreatePerson
{
    public class CreatePersonCommand : IRequestHandler<CreatePersonRequest, CreatePersonResponse>
    {
        #region Properties

        private IPersonRepository PersonRepository { get; }

        #endregion

        #region Constructors
        
        public CreatePersonCommand(IPersonRepository personRepository)
        {
            PersonRepository = personRepository;
        }
        
        #endregion

        #region Public Methods

        public async Task<CreatePersonResponse> Handle(CreatePersonRequest request, CancellationToken cancellationToken)
        {
            var person = CreatePersonAdapter.BuildToPerson(request);

            await PersonRepository.CreatePerson(person);

            return new CreatePersonResponse
            {
                Cpf = person.Cpf,
                Name = person.Name
            };
        }

        #endregion
        
    }
}