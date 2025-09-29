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
            try
            {
                var person = CreatePersonAdapter.BuildToPerson(request);

                await PersonRepository.CreatePerson(person);

                return new CreatePersonResponse
                {
                    Success = true,
                    Message = "Person created successfully.",
                    Cpf = person.Cpf,
                    Name = person.Name
                };

            }
            catch (Exception ex)
            {
                return new CreatePersonResponse
                {
                    Success = false,
                    Message = $"An error occurred while creating person: {ex.Message}"
                };
            }
        }

        #endregion
        
    }
}