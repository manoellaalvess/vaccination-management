using MediatR;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.Application.Command.DeleteVaccination
{
    public class DeleteVaccinationCommand : IRequestHandler<DeleteVaccinationRequest, DeleteVaccinationResponse>
    {
        #region Properties

        private IVaccinationRepository VaccinationRepository { get; }

        #endregion

        #region Constructors

        public DeleteVaccinationCommand(IVaccinationRepository vaccinationRepository)
        {
            VaccinationRepository = vaccinationRepository;
        }

        #endregion

        #region Public Methods

        public async Task<DeleteVaccinationResponse> Handle(DeleteVaccinationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var vaccination = await VaccinationRepository.GetByIdAsync(request.Id);
                if (vaccination == null)
                {
                    return new DeleteVaccinationResponse
                    {
                        Success = false,
                        Message = "Vaccination not found."
                    };
                }

                await VaccinationRepository.DeleteVaccinationAsync(request.Id);

                return new DeleteVaccinationResponse
                {
                    Success = true,
                    Message = "Vaccination deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new DeleteVaccinationResponse
                {
                    Success = false,
                    Message = $"An error occurred while deleting the vaccination: {ex.Message}"
                };
            }
        }

        #endregion
    }
}