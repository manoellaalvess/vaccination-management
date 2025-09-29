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
            try
            {
                var response = new GetAllVaccineResponse();

                var vaccines = await VaccineRepository.GetAllAsync();

                if (vaccines == null || !vaccines.Any())
                {
                    response.Message = "No vaccine found.";
                    response.Success = true;
                    response.Vaccines = new List<VaccineDto>();

                    return response;
                }

                var result = vaccines.Select(v => new VaccineDto
                {
                    VaccineId = v.VaccineId,
                    VaccineName = v.VaccineName
                }).ToList();

                return new GetAllVaccineResponse 
                {
                    Success = true,
                    Message = "Vaccines retrieved successfully.",
                    Vaccines = result
                };
            }
            catch (Exception ex)
            {
                return new GetAllVaccineResponse
                {
                    Success = false,
                    Message = $"An error occurred while retrieving all vaccines: {ex.Message}"
                };
            }
        }

        #endregion
    }
}