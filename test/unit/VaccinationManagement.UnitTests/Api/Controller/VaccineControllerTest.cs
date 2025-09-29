using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VaccinationManagement.Api.Controllers;
using VaccinationManagement.Application.Command.CreateVaccine;
using VaccinationManagement.Application.Queries.GetAllVaccine;
using VaccinationManagement.Domain.DTO;

namespace VaccinationManagement.UnitTests.Api.Controller
{
    public class VaccineControllerTest
    {
        private readonly Mock<IMediator> MediatorMock;
        private readonly VaccineController Controller;

        public VaccineControllerTest()
        {
            MediatorMock = new Mock<IMediator>();
            Controller = new VaccineController(MediatorMock.Object);
        }

        [Fact(DisplayName = "Should return success true when vaccine is created")]
        public async Task CreateVaccineShouldReturnSuccess()
        {
            // Arrange
            var request = new CreateVaccineRequest
            {
                VaccineName = "COVID-19 Vaccine"
            };

            var expectedResponse = new CreateVaccineResponse
            {
                Success = true,
                Message = "Vaccinne created successfully.",
                VaccineName = "COVID-19 Vaccine"
            };

            MediatorMock
                .Setup(m => m.Send(It.IsAny<CreateVaccineRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await Controller.CreateVaccine(request, CancellationToken.None);

            // Assert
            var okResult = result as OkObjectResult;
            var responseData = okResult!.Value.Should().BeOfType<CreateVaccineResponse>().Subject;

            responseData.Should().NotBeNull();
            responseData.Message.Should().Be("Vaccinne created successfully.");
            responseData.Success.Should().Be(true);
        }

        [Fact(DisplayName = "Should return success true when retrieving all vaccines")]
        public async Task GetAllVaccinesShouldReturnSuccess()
        {
            // Arrange
            var expectedResponse = new GetAllVaccineResponse
            {
                Success = true,
                Message = "Vaccines retrieved successfully.",
                Vaccines = new List<VaccineDto>
                {
                    new VaccineDto { VaccineId = 1, VaccineName = "COVID-19 Vaccine" }
                }
            };

            MediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllVaccineRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await Controller.GetAllVaccines(CancellationToken.None);

            // Assert
            var okResult = result as OkObjectResult;
            var responseData = okResult!.Value.Should().BeOfType<GetAllVaccineResponse>().Subject;

            responseData.Should().NotBeNull();
            responseData.Message.Should().Be("Vaccines retrieved successfully.");
            responseData.Success.Should().Be(true);
        }
    }
}