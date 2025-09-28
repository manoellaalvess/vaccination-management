using VaccinationManagement.Domain.DTO;
using VaccinationManagement.Domain.Entity;

namespace VaccinationManagement.Application.Queries.GetByCpf
{
    public class GetByCpfAdapter
    {
        public static GetByCpfResponse Adapt(Person person)
        {
            if (!person.Vaccinations.Any())
            {
                return new GetByCpfResponse
                {
                    Message = "Person has no vaccinations.",
                    Success = true,
                    Person = new PersonDto()
                };
            }

            var response = new GetByCpfResponse
            {
                Message = "Vaccination Card retrieved successfully.",
                Success = true,
                Person = new PersonDto
                {
                    Cpf = person.Cpf,
                    Name = person.Name,
                    Vaccinations = person.Vaccinations.Select(v => new VaccinationDto
                    {
                        VaccineName = v.VaccineName,
                        Dose = v.Dose,
                        ApplicationDate = v.VaccinationDate.ToString("dd-MM-yyyy")
                    }).ToList()
                }
            };

            return response;
        }
    }
}