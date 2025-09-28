using MediatR;
using VaccinationManagement.Domain.DTO;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.Application.Queries.GetVaccinationCard
{
    public class GetVaccinationCardQuery : IRequestHandler<GetVaccinationCardRequest, GetVaccinationCardResponse>
    {
        #region Properties

        private IPersonRepository PersonRepository { get; }

        #endregion

        #region Constructor

        public GetVaccinationCardQuery(IPersonRepository personRepository)
        {
            PersonRepository = personRepository;
        }

        #endregion

        #region Public Methods

        public async Task<GetVaccinationCardResponse> Handle(GetVaccinationCardRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var person = await PersonRepository.GetByCpfAsync(request.Cpf);

                if (person == null)
                {
                    return new GetVaccinationCardResponse
                    {
                        Message = "Person not found.",
                        Success = false,
                        Person = new PersonDto()
                    };
                }

                var result = GetVaccinationCardAdapter.Adapt(person);

                return result;
            }
            catch (Exception ex)
            {
                return new GetVaccinationCardResponse
                {
                    Success = false,
                    Message = $"An error occurred while retrieving vaccination card: {ex.Message}"
                };
            }
        }

        #endregion
    }
}