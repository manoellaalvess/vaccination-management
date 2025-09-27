using FluentValidation;

namespace VaccinationManagement.Application.Command.CreatePerson
{
    public class CreatePersonValidator : AbstractValidator<CreatePersonRequest>
    {
        public CreatePersonValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("CPF is required.")
                .Length(11).WithMessage("CPF must be 11 characters long.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name cannot exceed 200 characters.");
        }
    }
}