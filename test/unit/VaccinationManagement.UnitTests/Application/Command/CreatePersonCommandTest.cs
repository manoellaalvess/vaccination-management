using FluentAssertions;
using Moq;
using VaccinationManagement.Application.Command.CreatePerson;
using VaccinationManagement.Domain.Entity;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.UnitTests.Application.Command
{
    public class CreatePersonCommandTest
    {
        private readonly Mock<IPersonRepository> PersonRepositoryMock;
        private readonly CreatePersonCommand Handler;

        public CreatePersonCommandTest()
        {
            PersonRepositoryMock = new Mock<IPersonRepository>();
            Handler = new CreatePersonCommand(PersonRepositoryMock.Object);
        }

        [Fact(DisplayName = "Handle should return success when person is created")]
        public async Task CreatePersonShouldReturnSuccessWhenPersonIsCreated()
        {
            // Arrange
            var request = new CreatePersonRequest
            {
                Cpf = "12345678901",
                Name = "Maria da Silva"
            };

            PersonRepositoryMock
                .Setup(repo => repo.CreatePerson(It.IsAny<Person>()))
                .Returns(Task.CompletedTask); // só completa

            // Act
            var response = await Handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Message.Should().Be("Person created successfully.");
            response.Cpf.Should().Be("12345678901");
            response.Name.Should().Be("Maria da Silva");

            PersonRepositoryMock.Verify(repo => repo.CreatePerson(It.IsAny<Person>()), Times.Once);
        }

        [Fact(DisplayName = "Handle should return error when repository throws exception")]
        public async Task CreatePersonShouldReturnErrorWhenRepositoryThrowsException()
        {
            // Arrange
            var request = new CreatePersonRequest
            {
                Cpf = "98765432100",
                Name = "João Pereira"
            };

            PersonRepositoryMock
                .Setup(repo => repo.CreatePerson(It.IsAny<Person>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var response = await Handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeFalse();
            response.Message.Should().Contain("An error occurred while creating person");
            response.Message.Should().Contain("Database error");

            PersonRepositoryMock.Verify(repo => repo.CreatePerson(It.IsAny<Person>()), Times.Once);
        }
    }
}