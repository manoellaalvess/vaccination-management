using MediatR;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.Application.Command.DeletePerson
{
    public class DeletePersonCommand : IRequestHandler<DeletePersonRequest, DeletePersonResponse>
    {
        #region Properties

        private IPersonRepository PersonRepository { get; }

        #endregion

        #region Constructors
        
        public DeletePersonCommand(IPersonRepository personRepository)
        {
            PersonRepository = personRepository;
        }
        
        #endregion

        #region Public Methods

        public async Task<DeletePersonResponse> Handle(DeletePersonRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await PersonRepository.DeletePerson(request.Cpf);

                return new DeletePersonResponse
                {
                    Success = true,
                    Message = "Person deleted successfully.",
                    Cpf = request.Cpf
                };

            }
            catch (Exception ex)
            {
                return new DeletePersonResponse
                {
                    Success = false,
                    Message = $"Error deleting person: {ex.Message}",
                    Cpf = request.Cpf
                };
            }
        }

        #endregion
    }
}