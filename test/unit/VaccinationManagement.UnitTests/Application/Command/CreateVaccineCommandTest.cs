using FluentAssertions;
using Moq;
using VaccinationManagement.Application.Command.CreateVaccine;
using VaccinationManagement.Domain.Entity;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.UnitTests.Application.Command
{
    public class CreateVaccineCommandTest
    {
        private readonly Mock<IVaccineRepository> VaccineRepositoryMock;
        private readonly CreateVaccineCommand Handler;

        public CreateVaccineCommandTest()
        {
            VaccineRepositoryMock = new Mock<IVaccineRepository>();
            Handler = new CreateVaccineCommand(VaccineRepositoryMock.Object);
        }

        [Fact(DisplayName = "CreateVaccineCommand should return success when vaccine is created")]
        public async Task CreateVaccineCommandShouldReturnSuccess()
        {
            // Arrange
            var request = new CreateVaccineRequest
            {
                VaccineName = "COVID-19"
            };

            VaccineRepositoryMock
                .Setup(repo => repo.CreateVaccine(It.IsAny<Vaccine>()))
                .Returns(Task.CompletedTask);

            // Act
            var response = await Handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Message.Should().Be("Vaccine created successfully.");
            response.VaccineName.Should().Be("COVID-19");
        }

         [Fact]
        public async Task Handle_ShouldReturnError_WhenRepositoryThrowsException()
        {
            // Arrange
            var request = new CreateVaccineRequest
            {
                VaccineName = "Influenza"
            };

            VaccineRepositoryMock
                .Setup(repo => repo.CreateVaccine(It.IsAny<Vaccine>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var response = await Handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeFalse();
            response.Message.Should().Contain("An error occurred while creating vaccine");
            response.Message.Should().Contain("Database error");
        }
    }
}