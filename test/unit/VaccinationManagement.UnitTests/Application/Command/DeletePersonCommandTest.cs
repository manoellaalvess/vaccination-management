using FluentAssertions;
using Moq;
using VaccinationManagement.Application.Command.DeletePerson;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.UnitTests.Application.Command
{
    public class DeletePersonCommandTest
    {
        private readonly Mock<IPersonRepository> PersonRepositoryMock;
        private readonly DeletePersonCommand Handler;

        public DeletePersonCommandTest()
        {
            PersonRepositoryMock = new Mock<IPersonRepository>();
            Handler = new DeletePersonCommand(PersonRepositoryMock.Object);
        }

        [Fact(DisplayName = "Handle should return success when person is deleted")]
        public async Task DeletePersonShouldReturnSuccessWhenPersonIsDeleted()
        {
            // Arrange
            var request = new DeletePersonRequest
            {
                Cpf = "12345678901"
            };

            PersonRepositoryMock
                .Setup(repo => repo.DeletePerson(It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            var response = await Handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Message.Should().Be("Person deleted successfully.");
            response.Cpf.Should().Be("12345678901");
        }

        [Fact(DisplayName = "Handle should return error when repository throws exception")]
        public async Task DeletePersonShouldReturnErrorWhenRepositoryThrowsException()
        {
            // Arrange
            var request = new DeletePersonRequest
            {
                Cpf = "98765432100"
            };

            PersonRepositoryMock
                .Setup(repo => repo.DeletePerson(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var response = await Handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeFalse();
            response.Message.Should().Contain("Error deleting person");
            response.Message.Should().Contain("Database error");
            response.Cpf.Should().Be("98765432100");
        }
    }
}