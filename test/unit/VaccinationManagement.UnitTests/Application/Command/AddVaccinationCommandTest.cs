using FluentAssertions;
using Moq;
using VaccinationManagement.Application.Command.AddVaccination;
using VaccinationManagement.Domain.Entity;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.UnitTests.Application.Command
{
    public class AddVaccinationCommandTest
    {
        private readonly Mock<IPersonRepository> PersonRepositoryMock;
        private readonly Mock<IVaccineRepository> VaccineRepositoryMock;
        private readonly Mock<IVaccinationRepository> VaccinationRepositoryMock;
        private readonly AddVaccinationCommand Handler;

        public AddVaccinationCommandTest()
        {
            PersonRepositoryMock = new Mock<IPersonRepository>();
            VaccineRepositoryMock = new Mock<IVaccineRepository>();
            VaccinationRepositoryMock = new Mock<IVaccinationRepository>();

            Handler = new AddVaccinationCommand(
                PersonRepositoryMock.Object,
                VaccineRepositoryMock.Object,
                VaccinationRepositoryMock.Object
            );
        }

        [Fact(DisplayName = "Handle should return success when vaccination is added")]
        public async Task HandleShouldReturnSuccessWhenVaccinationIsAdded()
        {
            // Arrange
            var person = new Person("12345678901", "Maria da Silva");
            var vaccine = new Vaccine("COVID-19");

            PersonRepositoryMock.Setup(r => r.GetByCpfAsync("12345678901"))
                .ReturnsAsync(person);

            VaccineRepositoryMock.Setup(r => r.GetByVaccineId(1))
                .ReturnsAsync(vaccine);

            VaccinationRepositoryMock.Setup(r => r.AddVaccination(It.IsAny<Vaccination>()))
                .Returns(Task.CompletedTask);

            var request = new AddVaccinationRequest
            {
                PersonCpf = "12345678901",
                VaccineId = 1,
                Dose = 1
            };

            // Act
            var response = await Handler.Handle(request, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
            response.Message.Should().Be("Vaccination added successfully");
        }

        [Fact(DisplayName = "Handle should return error when person not found")]
        public async Task HandleShouldReturnErrorWhenPersonNotFound()
        {
            // Arrange
            PersonRepositoryMock.Setup(r => r.GetByCpfAsync("999"))
                .ReturnsAsync((Person)null);

            var request = new AddVaccinationRequest
            {
                PersonCpf = "999",
                VaccineId = 1,
                Dose = 1
            };

            // Act
            var response = await Handler.Handle(request, CancellationToken.None);

            // Assert
            response.Success.Should().BeFalse();
            response.Message.Should().Be("Person not found");
        }

        [Fact(DisplayName = "Handle should return error when vaccine not found")]
        public async Task HandleShouldReturnErrorWhenVaccineNotFound()
        {
            // Arrange
            var person = new Person("12345678901", "Maria da Silva");

            PersonRepositoryMock.Setup(r => r.GetByCpfAsync("12345678901"))
                .ReturnsAsync(person);

            VaccineRepositoryMock.Setup(r => r.GetByVaccineId(1))
                .ReturnsAsync((Vaccine)null);

            var request = new AddVaccinationRequest
            {
                PersonCpf = "12345678901",
                VaccineId = 1,
                Dose = 1
            };

            // Act
            var response = await Handler.Handle(request, CancellationToken.None);

            // Assert
            response.Success.Should().BeFalse();
            response.Message.Should().Be("Vaccine not found");
        }

        [Fact(DisplayName = "Handle should return error when dose already applied")]
        public async Task HandleShouldReturnErrorWhenDoseAlreadyApplied()
        {
            // Arrange
            var person = new Person("12345678901", "Maria da Silva");
            person.AddVaccination(new Vaccination("12345678901", 1, "COVID-19", DateTime.Now, 1));

            var vaccine = new Vaccine("COVID-19");

            PersonRepositoryMock.Setup(r => r.GetByCpfAsync("12345678901"))
                .ReturnsAsync(person);

            VaccineRepositoryMock.Setup(r => r.GetByVaccineId(1))
                .ReturnsAsync(vaccine);

            var request = new AddVaccinationRequest
            {
                PersonCpf = "12345678901",
                VaccineId = 1,
                Dose = 1
            };

            // Act
            var response = await Handler.Handle(request, CancellationToken.None);

            // Assert
            response.Success.Should().BeFalse();
            response.Message.Should().Be("Dose already applied");
        }

        [Fact(DisplayName = "Handle should return error when invalid dose order")]
        public async Task HandleShouldReturnErrorWhenInvalidDoseOrder()
        {
            // Arrange
            var person = new Person("12345678901", "Maria da Silva");
            var vaccine = new Vaccine("COVID-19");

            PersonRepositoryMock.Setup(r => r.GetByCpfAsync("12345678901"))
                .ReturnsAsync(person);

            VaccineRepositoryMock.Setup(r => r.GetByVaccineId(1))
                .ReturnsAsync(vaccine);

            var request = new AddVaccinationRequest
            {
                PersonCpf = "12345678901",
                VaccineId = 1,
                Dose = 2
            };

            // Act
            var response = await Handler.Handle(request, CancellationToken.None);

            // Assert
            response.Success.Should().BeFalse();
            response.Message.Should().Contain("Invalid dose order");
        }

        [Fact(DisplayName = "Handle should return error when repository throws exception")]
        public async Task HandleShouldReturnErrorWhenRepositoryThrowsException()
        {
            // Arrange
            PersonRepositoryMock.Setup(r => r.GetByCpfAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Database error"));

            var request = new AddVaccinationRequest
            {
                PersonCpf = "12345678901",
                VaccineId = 1,
                Dose = 1
            };

            // Act
            var response = await Handler.Handle(request, CancellationToken.None);

            // Assert
            response.Success.Should().BeFalse();
            response.Message.Should().Contain("An error occurred while adding the vaccination");
            response.Message.Should().Contain("Database error");
        }
    }
}