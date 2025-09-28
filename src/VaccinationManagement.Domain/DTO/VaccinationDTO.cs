namespace VaccinationManagement.Domain.DTO
{
    public class VaccinationDto
    {
        public int VaccineId { get; set; }
        public string VaccineName { get; set; }
        public int Dose { get; set; }
        public string ApplicationDate { get; set; }
    }
}