using MediatR;
using VaccinationManagement.Domain.DTO;
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
            try
            {
                var response = new GetAllPeopleResponse
                {
                    Success = true,
                    Message = "People retrieved successfully."
                };

                var people = await PersonRepository.GetAllAsync();

                response.People = people.Select(p => new PersonDto
                {
                    Cpf = p.Cpf,
                    Name = p.Name,
                    Vaccinations = p.Vaccinations?.Select(v => new VaccinationDto
                    {
                        VaccinationId = v.VaccinationId,
                        VaccineId = v.VaccineId,
                        VaccineName = v.VaccineName,
                        Dose = v.Dose,
                        ApplicationDate = v.VaccinationDate.ToString("dd-MM-yyyy")
                    }).ToList() ?? new List<VaccinationDto>()
                }).ToList();

                return response;
            }
            catch (Exception ex)
            {
                return new GetAllPeopleResponse
                {
                    Success = false,
                    Message = $"An error occurred while retrieving people: {ex.Message}"
                };
            }
        }
    }
}