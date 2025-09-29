using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VaccinationManagement.Api.Controllers;
using VaccinationManagement.Application.Command.AddVaccination;
using VaccinationManagement.Application.Command.DeleteVaccination;

namespace VaccinationManagement.UnitTests.Api.Controller
{
    public class VaccinationControllerTest
    {
        private readonly Mock<IMediator> MediatorMock;
        private readonly VaccinationController Controller;

        public VaccinationControllerTest()
        {
            MediatorMock = new Mock<IMediator>();
            Controller = new VaccinationController(MediatorMock.Object);
        }

        [Fact(DisplayName = "Should return success true when vaccination is created")]
        public async Task CreateVaccinationShouldReturnSuccess()
        {
            // Arrange
            var request = new AddVaccinationRequest
            {
                PersonCpf = "12345678901",
                VaccineId = 1,
                Dose = 1
            };

            var expectedResponse = new AddVaccinationResponse
            {
                Success = true,
                Message = "Vaccination added successfully"
            };

            MediatorMock
                .Setup(m => m.Send(It.IsAny<AddVaccinationRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await Controller.CreateVaccination(request, CancellationToken.None);

            // Assert
            var okResult = result as OkObjectResult;
            var responseData = okResult!.Value.Should().BeOfType<AddVaccinationResponse>().Subject;

            responseData.Should().NotBeNull();
            responseData.Message.Should().Be("Vaccination added successfully");
            responseData.Success.Should().Be(true);
        }

        [Fact(DisplayName = "Should return success true when vaccination is deleted")]
        public async Task DeleteVaccinationShouldReturnSuccess()
        {
            // Arrange
            var request = new DeleteVaccinationRequest
            {
                Id = 1
            };

            var expectedResponse = new DeleteVaccinationResponse
            {
                Success = true,
                Message = "Vaccination deleted successfully."
            };

            MediatorMock
                .Setup(m => m.Send(It.IsAny<DeleteVaccinationRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await Controller.DeleteVaccination(1, CancellationToken.None);

            // Assert
            var okResult = result as OkObjectResult;
            var responseData = okResult!.Value.Should().BeOfType<DeleteVaccinationResponse>().Subject;

            responseData.Should().NotBeNull();
            responseData.Message.Should().Be("Vaccination deleted successfully.");
            responseData.Success.Should().Be(true);
        }
    }
}