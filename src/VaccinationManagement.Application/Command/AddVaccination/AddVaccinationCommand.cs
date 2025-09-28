using MediatR;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.Application.Command.AddVaccination
{
    public class AddVaccinationCommand : IRequestHandler<AddVaccinationRequest, AddVaccinationResponse>
    {
        #region Properties

        private readonly IPersonRepository PersonRepository;
        private readonly IVaccineRepository VaccineRepository;
        private readonly IVaccinationRepository VaccinationRepository;

        #endregion

        #region Constructor

        public AddVaccinationCommand(IPersonRepository personRepository, IVaccineRepository vaccineRepository, IVaccinationRepository vaccinationRepository)
        {
            PersonRepository = personRepository;
            VaccineRepository = vaccineRepository;
            VaccinationRepository = vaccinationRepository;
        }

        #endregion

        #region Public Methods

        public async Task<AddVaccinationResponse> Handle(AddVaccinationRequest request, CancellationToken cancellationToken)
        {
            // Person validation
            var person = await PersonRepository.GetByCpfAsync(request.PersonCpf);
            if (person == null)
                return new AddVaccinationResponse { Success = false, Message = "Person not found" };

            // Vaccine validation
            var vaccine = await VaccineRepository.GetByVaccineId(request.VaccineId);
            if (vaccine == null)
                return new AddVaccinationResponse { Success = false, Message = "Vaccine not found" };

            // Dose validation
            var vaccinations = person.Vaccinations.Where(v => v.VaccineId == request.VaccineId).ToList();
            if (vaccinations.Any() && vaccinations.Any(v => v.Dose == request.Dose))
                return new AddVaccinationResponse { Success = false, Message = "Dose already applied" };

            if (request.Dose != vaccinations.Count + 1)
                return new AddVaccinationResponse { Success = false, Message = "Invalid dose order" };

            var vaccination = AddVaccinationAdapter.BuildToVaccination(request);

            await VaccinationRepository.AddVaccination(vaccination);

            return new AddVaccinationResponse { Success = true, Message = "Vaccination added successfully" };
        }

        #endregion
    }
}