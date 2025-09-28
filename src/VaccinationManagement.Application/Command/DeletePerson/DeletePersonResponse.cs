namespace VaccinationManagement.Application.Command.DeletePerson
{
    public class DeletePersonResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Cpf { get; set; }
    }
}