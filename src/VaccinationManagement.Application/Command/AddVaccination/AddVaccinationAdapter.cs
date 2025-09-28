using VaccinationManagement.Domain.Entity;

namespace VaccinationManagement.Application.Command.AddVaccination
{
    public class AddVaccinationAdapter
    {
        public static Vaccination BuildToVaccination(AddVaccinationRequest request, string vaccineName)
        {
            return new Vaccination(
                request.PersonCpf,
                request.VaccineId,
                vaccineName,
                DateTime.UtcNow,
                request.Dose
            );
        }
    }
}