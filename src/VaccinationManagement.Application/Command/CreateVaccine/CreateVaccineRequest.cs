using MediatR;

namespace VaccinationManagement.Application.Command.CreateVaccine
{
    public class CreateVaccineRequest : IRequest<CreateVaccineResponse>
    {
        public string VaccineName { get; set; }
    }
}