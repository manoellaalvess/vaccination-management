using MediatR;
using VaccinationManagement.Domain.DTO;
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

            var result = vaccines.Select(v => new VaccineDto
            {
                VaccineId = v.VaccineId,
                VaccineName = v.VaccineName
            }).ToList();

            return new GetAllVaccineResponse { Vaccines = result };
        }

        #endregion
    }
}