using FluentAssertions;
using Moq;
using VaccinationManagement.Application.Command.DeleteVaccination;
using VaccinationManagement.Domain.Entity;
using VaccinationManagement.Domain.Repository;

namespace VaccinationManagement.UnitTests.Application.Command
{
    public class DeleteVaccinationCommandTest
    {
        private readonly Mock<IVaccinationRepository> _vaccinationRepositoryMock;
        private readonly DeleteVaccinationCommand _handler;

        public DeleteVaccinationCommandTest()
        {
            _vaccinationRepositoryMock = new Mock<IVaccinationRepository>();
            _handler = new DeleteVaccinationCommand(_vaccinationRepositoryMock.Object);
        }

        [Fact(DisplayName = "Handle should return success when vaccination is deleted")]
        public async Task DeleteVaccinationShouldReturnSuccessWhenVaccinationIsDeleted()
        {
            // Arrange
            var vaccination = new Vaccination("12345678901", 1, "Covid-19", DateTime.Now, 1);

            _vaccinationRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(vaccination);

            _vaccinationRepositoryMock
                .Setup(r => r.DeleteVaccinationAsync(1))
                .Returns(Task.CompletedTask);

            var request = new DeleteVaccinationRequest { Id = 1 };

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
            response.Message.Should().Be("Vaccination deleted successfully.");
        }

        [Fact(DisplayName = "Handle should return error when vaccination not found")]
        public async Task DeleteVaccinationShouldReturnErrorWhenVaccinationNotFound()
        {
            // Arrange
            _vaccinationRepositoryMock
                .Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Vaccination)null);

            var request = new DeleteVaccinationRequest { Id = 999 };

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response.Success.Should().BeFalse();
            response.Message.Should().Be("Vaccination not found.");
        }

        [Fact(DisplayName = "Handle should return error when repository throws exception")]
        public async Task DeleteVaccinationShouldReturnErrorWhenRepositoryThrowsException()
        {
            // Arrange
            _vaccinationRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Database error"));

            var request = new DeleteVaccinationRequest { Id = 1 };

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response.Success.Should().BeFalse();
            response.Message.Should().Contain("An error occurred while deleting the vaccination");
            response.Message.Should().Contain("Database error");
        }
    }
}