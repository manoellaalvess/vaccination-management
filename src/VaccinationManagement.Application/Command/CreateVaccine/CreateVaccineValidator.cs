using FluentValidation;

namespace VaccinationManagement.Application.Command.CreateVaccine
{
    public class CreateVaccineValidator : AbstractValidator<CreateVaccineRequest>
    {
        public CreateVaccineValidator()
        {
            RuleFor(x => x.VaccineName)
                .NotEmpty().WithMessage("Vaccine Name is required.")
                .MaximumLength(100).WithMessage("Vaccine Name cannot exceed 100 characters long.");
        }
    }
}