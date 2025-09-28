using VaccinationManagement.Domain.DTO;
using VaccinationManagement.Domain.Entity;

namespace VaccinationManagement.Application.Queries.GetVaccinationCard
{
    public class GetVaccinationCardAdapter
    {
        public static GetVaccinationCardResponse Adapt(Person person)
        {
            if (!person.Vaccinations.Any())
            {
                return new GetVaccinationCardResponse
                {
                    Message = "Person has no vaccinations.",
                    Success = true,
                    Person = new PersonDto()
                };
            }

            var response = new GetVaccinationCardResponse
            {
                Message = "Vaccination Card retrieved successfully.",
                Success = true,
                Person = new PersonDto
                {
                    Cpf = person.Cpf,
                    Name = person.Name,
                    Vaccinations = person.Vaccinations.Select(v => new VaccinationDto
                    {
                        VaccinationId = v.VaccinationId,
                        VaccineName = v.VaccineName,
                        VaccineId = v.VaccineId,
                        Dose = v.Dose,
                        ApplicationDate = v.VaccinationDate.ToString("dd-MM-yyyy")
                    }).ToList()
                }
            };

            return response;
        }
    }
}