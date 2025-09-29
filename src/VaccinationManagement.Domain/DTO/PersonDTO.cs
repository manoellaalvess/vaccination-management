namespace VaccinationManagement.Domain.DTO
{
    public class PersonDto
    {
        public string Cpf { get; set; }
        public string Name { get; set; }
        public List<VaccinationDto> Vaccinations { get; set; } = new List<VaccinationDto>();
    }
}