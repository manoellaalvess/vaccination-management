namespace VaccinationManagement.Domain.Entity
{
    public class Vaccine
    {
        public int VaccineId { get; private set; }
        public string VaccineName { get; private set; }

        private readonly List<Vaccination> _vaccinations = new();
        public IReadOnlyCollection<Vaccination> Vaccinations => _vaccinations.AsReadOnly();

        protected Vaccine() { }

        public Vaccine(string vaccineName)
        {
            if (string.IsNullOrWhiteSpace(vaccineName))
                throw new ArgumentException("Nome da vacina é obrigatório.", nameof(vaccineName));

            VaccineName = vaccineName;
        }
    }
}