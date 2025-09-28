using VaccinationManagement.Domain.Entity;

namespace VaccinationManagement.Application.Command.AddVaccination
{
    public class AddVaccinationAdapter
    {
        public static Vaccination BuildToVaccination(AddVaccinationRequest request)
        {
            return new Vaccination(
                request.PersonCpf,
                request.VaccineId,
                DateTime.UtcNow,
                request.Dose
            );
        }
    }
}