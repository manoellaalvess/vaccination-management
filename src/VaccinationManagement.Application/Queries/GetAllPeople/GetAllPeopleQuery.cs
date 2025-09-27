using MediatR;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.Application.Queries.GetAllPeople
{
    public class GetAllPeopleQuery : IRequestHandler<GetAllPeopleRequest, GetAllPeopleResponse>
    {
        #region Properties

        private IPersonRepository PersonRepository { get; }

        #endregion

        #region Constructor

        public GetAllPeopleQuery(IPersonRepository personRepository)
        {
            PersonRepository = personRepository;
        }

        #endregion

        public async Task<GetAllPeopleResponse> Handle(GetAllPeopleRequest request, CancellationToken cancellationToken)
        {
            var people = await PersonRepository.GetAllAsync();
            
            return new GetAllPeopleResponse { People = people };
        }
    }
}