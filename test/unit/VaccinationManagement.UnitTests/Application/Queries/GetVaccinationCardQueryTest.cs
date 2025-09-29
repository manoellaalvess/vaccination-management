using Moq;
using VaccinationManagement.Application.Queries.GetVaccinationCard;
using VaccinationManagement.Domain.Repository;
using VaccinationManagement.Domain.Entity;
using FluentAssertions;

namespace VaccinationManagement.UnitTests.Application.Queries
{
    public class GetVaccinationCardQueryTest
    {
        private readonly Mock<IPersonRepository> PersonRepositoryMock;
        private readonly GetVaccinationCardQuery Handler;

        public GetVaccinationCardQueryTest()
        {
            PersonRepositoryMock = new Mock<IPersonRepository>();
            Handler = new GetVaccinationCardQuery(PersonRepositoryMock.Object);
        }

        [Fact(DisplayName = "Handle should return vaccination card when person exists")]
        public async Task HandleShouldReturnVaccinationCardWhenPersonExists()
        {
            // Arrange
            var person = new Person("12345678901", "Maria da Silva");
            person.AddVaccination(new Vaccination("12345678901", 1, "Covid-19", DateTime.Parse("2025-09-29"), 1));

            PersonRepositoryMock
                .Setup(repo => repo.GetByCpfAsync("12345678901"))
                .ReturnsAsync(person);

            var request = new GetVaccinationCardRequest { Cpf = "12345678901" };

            // Act
            var response = await Handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Person.Should().NotBeNull();
            response.Person.Cpf.Should().Be("12345678901");
            response.Person.Name.Should().Be("Maria da Silva");
            response.Person.Vaccinations.Should().HaveCount(1);
        }

        [Fact(DisplayName = "Handle should return not found when person does not exist")]
        public async Task HandleShouldReturnNotFoundWhenPersonDoesNotExist()
        {
            // Arrange
            PersonRepositoryMock
                .Setup(repo => repo.GetByCpfAsync("99999999999"))
                .ReturnsAsync((Person)null);

            var request = new GetVaccinationCardRequest { Cpf = "99999999999" };

            // Act
            var response = await Handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeFalse();
            response.Message.Should().Be("Person not found.");
            response.Person.Should().NotBeNull();
            response.Person.Cpf.Should().BeNullOrEmpty();
            response.Person.Name.Should().BeNullOrEmpty();
            response.Person.Vaccinations.Should().BeEmpty();

            PersonRepositoryMock.Verify(repo => repo.GetByCpfAsync("99999999999"), Times.Once);
        }

        [Fact(DisplayName = "Handle should return error when repository throws exception")]
        public async Task HandleShouldReturnErrorWhenRepositoryThrowsException()
        {
            // Arrange
            PersonRepositoryMock
                .Setup(repo => repo.GetByCpfAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Database error"));

            var request = new GetVaccinationCardRequest { Cpf = "12345678901" };

            // Act
            var response = await Handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeFalse();
            response.Message.Should().Contain("An error occurred while retrieving vaccination card");
            response.Message.Should().Contain("Database error");

            PersonRepositoryMock.Verify(repo => repo.GetByCpfAsync("12345678901"), Times.Once);
        }
    }
}