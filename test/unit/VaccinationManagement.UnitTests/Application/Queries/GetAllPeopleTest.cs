using FluentAssertions;
using Moq;
using VaccinationManagement.Application.Queries.GetAllPeople;
using VaccinationManagement.Domain.Entity;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.UnitTests.Application.Queries
{
    public class GetAllPeopleQueryTest
    {
        private readonly Mock<IPersonRepository> PersonRepositoryMock;
        private readonly GetAllPeopleQuery Handler;

        public GetAllPeopleQueryTest()
        {
            PersonRepositoryMock = new Mock<IPersonRepository>();
            Handler = new GetAllPeopleQuery(PersonRepositoryMock.Object);
        }

        [Fact(DisplayName = "Should return people when repository has data")]
        public async Task ShouldReturnPeople()
        {
            // Arrange
            var maria = new Person("12345678901", "Maria da Silva");
            maria.AddVaccination(new Vaccination("12345678901", 1, "Covid-19", DateTime.Parse("2025-09-28"), 1));

            var people = new List<Person> { maria };

            PersonRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(people);

            // Act
            var response = await Handler.Handle(new GetAllPeopleRequest(), CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Message.Should().Be("People retrieved successfully.");
            response.People.Should().HaveCount(1);
        }

        [Fact(DisplayName = "Should return success when repository has no data")]
        public async Task ShouldReturnSuccessWhenNoData()
        {
            // Arrange
            PersonRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Person>());

            // Act
            var response = await Handler.Handle(new GetAllPeopleRequest(), CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Message.Should().Be("No person found.");
            response.People.Should().BeEmpty();
        }
    }
}