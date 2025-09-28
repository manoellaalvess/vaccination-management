namespace VaccinationManagement.Application.Command.CreatePerson
{
    public class CreatePersonResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
    }
}