using MediatR;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.Application.Command.CreateVaccine
{
    public class CreateVaccineCommand : IRequestHandler<CreateVaccineRequest, CreateVaccineResponse>
    {
        #region Properties

        private IVaccineRepository VaccineRepository { get; }

        #endregion

        #region Constructors

        public CreateVaccineCommand(IVaccineRepository vaccineRepository)
        {
            VaccineRepository = vaccineRepository;
        }

        #endregion

        #region Public Methods

        public async Task<CreateVaccineResponse> Handle(CreateVaccineRequest request, CancellationToken cancellationToken)
        {
            var vaccine = CreateVaccineAdapter.BuildToVaccine(request);

            await VaccineRepository.CreateVaccine(vaccine);

            return new CreateVaccineResponse
            {
                VaccineName = vaccine.VaccineName,
            };
        }

        #endregion
    }
}