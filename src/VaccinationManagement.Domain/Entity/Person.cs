namespace VaccinationManagement.Domain.Entity
{
    public class Person
    {
        public string Cpf { get; set; }
        public string Name { get; set; }

        private readonly List<Vaccination> _vaccinations = new();
        public IReadOnlyCollection<Vaccination> Vaccinations => _vaccinations.AsReadOnly();

        protected Person() { }

        public Person(string cpf, string name)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF é obrigatório.", nameof(cpf));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome é obrigatório.", nameof(name));

            Cpf = cpf;
            Name = name;
        }

        public void AddVaccination(Vaccination vaccination)
        {
            if (vaccination == null)
                throw new ArgumentNullException(nameof(vaccination));

            _vaccinations.Add(vaccination);
        }

        public void RemoveVaccination(Vaccination vaccination)
        {
            if (vaccination == null)
                throw new ArgumentNullException(nameof(vaccination));

            _vaccinations.Remove(vaccination);
        }
    }
}