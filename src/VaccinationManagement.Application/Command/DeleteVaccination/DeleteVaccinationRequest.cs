using MediatR;

namespace VaccinationManagement.Application.Command.DeleteVaccination
{
    public class DeleteVaccinationRequest : IRequest<DeleteVaccinationResponse>
    {
        public int Id { get; set; }
    }
}