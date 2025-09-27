using MediatR;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.Application.Queries.GetAllVaccine
{
    public class GetAllVaccineQuery : IRequestHandler<GetAllVaccineRequest, GetAllVaccineResponse>
    {
        #region Properties

        private IVaccineRepository VaccineRepository { get; }

        #endregion

        #region Constructors

        public GetAllVaccineQuery(IVaccineRepository vaccineRepository)
        {
            VaccineRepository = vaccineRepository;
        }

        #endregion

        #region Public Methods

        public async Task<GetAllVaccineResponse> Handle(GetAllVaccineRequest request, CancellationToken cancellationToken)
        {
            var vaccines = await VaccineRepository.GetAllAsync();

            return new GetAllVaccineResponse { Vaccines = vaccines};
        }

        #endregion
    }
}