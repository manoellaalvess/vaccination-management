namespace VaccinationManagement.Domain.Entity
{
    public class Vaccination
    {
        public int VaccinationId { get; private set; }
        public string PersonCpf { get; private set; }
        public int VaccineId { get; private set; }
        public DateTime VaccinationDate { get; private set; }
        public int Dose { get; private set; }

        public Person Person { get; private set; }
        public Vaccine Vaccine { get; private set; }

        protected Vaccination() { }

        public Vaccination(string personCpf, int vaccineId, DateTime vaccinationDate, int dose)
        {
            if (string.IsNullOrWhiteSpace(personCpf))
                throw new ArgumentException("CPF é obrigatório.", nameof(personCpf));

            if (vaccineId <= 0)
                throw new ArgumentException("Identificador de vacina inválido.", nameof(vaccineId));

            if (vaccinationDate > DateTime.UtcNow)
                throw new ArgumentException("Data de vacinação não pode ser futura.", nameof(vaccinationDate));

            if (dose <= 0)
                throw new ArgumentException("Dose deve ser maior que zero.", nameof(dose));

            PersonCpf = personCpf;
            VaccineId = vaccineId;
            VaccinationDate = vaccinationDate;
            Dose = dose;
        }
    }
}