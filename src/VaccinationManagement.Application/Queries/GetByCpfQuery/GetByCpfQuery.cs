using MediatR;
using VaccinationManagement.Domain.DTO;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.Application.Queries.GetByCpf
{
    public class GetByCpfQuery : IRequestHandler<GetByCpfRequest, GetByCpfResponse>
    {
        #region Properties

        private IPersonRepository PersonRepository { get; }

        #endregion

        #region Constructor

        public GetByCpfQuery(IPersonRepository personRepository)
        {
            PersonRepository = personRepository;
        }

        #endregion

        #region Public Methods

        public async Task<GetByCpfResponse> Handle(GetByCpfRequest request, CancellationToken cancellationToken)
        {
            var person = await PersonRepository.GetByCpfAsync(request.Cpf);

            if (person == null)
            {
                return new GetByCpfResponse
                {
                    Message = "Person not found.",
                    Success = false,
                    Person = new PersonDto()
                };
            }

            var result = GetByCpfAdapter.Adapt(person);

            return result;
        }

        #endregion
    }
}