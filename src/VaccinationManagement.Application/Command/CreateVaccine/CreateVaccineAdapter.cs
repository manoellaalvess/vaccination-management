using VaccinationManagement.Domain.Entity;

namespace VaccinationManagement.Application.Command.CreateVaccine
{
    public class CreateVaccineAdapter
    {
        public static Vaccine BuildToVaccine(CreateVaccineRequest request)
        {
            return new Vaccine(request.VaccineName);
        }
    }
}