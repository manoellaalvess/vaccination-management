using System.Text.Json.Serialization;
using MediatR;

namespace VaccinationManagement.Application.Command.AddVaccination
{
    public class AddVaccinationRequest : IRequest<AddVaccinationResponse>
    {
        [JsonPropertyName("personCpf")]
        public string PersonCpf { get; set; }

        [JsonPropertyName("vaccineId")]
        public int VaccineId { get; set; }

        [JsonPropertyName("dose")]
        public int Dose { get; set; }
    }
}