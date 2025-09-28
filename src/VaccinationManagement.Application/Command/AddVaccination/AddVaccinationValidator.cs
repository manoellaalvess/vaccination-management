using FluentValidation;

namespace VaccinationManagement.Application.Command.AddVaccination
{
    public class AddVaccinationValidator : AbstractValidator<AddVaccinationRequest>
    {
        public AddVaccinationValidator()
        {
            RuleFor(x => x.PersonCpf)
                .NotEmpty().WithMessage("Person CPF is required.")
                .Length(11).WithMessage("Person CPF must be 11 characters long.");

            RuleFor(x => x.VaccineId)
                .NotNull().WithMessage("Vaccine ID is required.");

            RuleFor(x => x.Dose)
                .NotNull()
                .GreaterThan(0).WithMessage("Dose must be greater than 0.");
        }
    }
}