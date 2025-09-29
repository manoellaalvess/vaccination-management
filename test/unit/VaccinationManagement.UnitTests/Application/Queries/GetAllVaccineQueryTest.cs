using FluentAssertions;
using Moq;
using VaccinationManagement.Application.Queries.GetAllVaccine;
using VaccinationManagement.Domain.Entity;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.UnitTests.Application.Queries
{
    public class GetAllVaccineQueryTest
    {
        private readonly Mock<IVaccineRepository> VaccineRepositoryMock;
        private readonly GetAllVaccineQuery Handler;

        public GetAllVaccineQueryTest()
        {
            VaccineRepositoryMock = new Mock<IVaccineRepository>();
            Handler = new GetAllVaccineQuery(VaccineRepositoryMock.Object);
        }

        [Fact(DisplayName = "Get all vaccines should return vaccines when repository has data")]
        public async Task GetAllVaccinesShouldReturnList()
        {
            // Arrange
            var vaccines = new List<Vaccine>
            {
                new Vaccine("COVID-19"),
                new Vaccine("Influenza")
            };

            VaccineRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(vaccines);

            var request = new GetAllVaccineRequest();

            // Act
            var response = await Handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Message.Should().Be("Vaccines retrieved successfully.");
            response.Vaccines.Should().HaveCount(2);
        }

        [Fact(DisplayName = "Get all vaccines should return empty list when repository has no data")]
        public async Task GetAllVaccinesShouldReturnEmptyList()
        {
            // Arrange
            VaccineRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Vaccine>());

            var request = new GetAllVaccineRequest();

            // Act
            var response = await Handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Message.Should().Be("No vaccine found.");
            response.Vaccines.Should().BeEmpty();
        }

        [Fact(DisplayName = "Get all vaccines should return error when repository throws exception")]
        public async Task GetAllVaccineShouldReturnError()
        {
            // Arrange
            VaccineRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ThrowsAsync(new Exception("Database connection failed"));

            var request = new GetAllVaccineRequest();

            // Act
            var response = await Handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeFalse();
            response.Message.Should().Contain("An error occurred while retrieving all vaccines");
            response.Message.Should().Contain("Database connection failed");
        }
    }
}