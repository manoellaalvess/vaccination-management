using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VaccinationManagement.Api.Controllers;
using VaccinationManagement.Application.Command.CreatePerson;
using VaccinationManagement.Application.Command.DeletePerson;
using VaccinationManagement.Application.Queries.GetAllPeople;
using VaccinationManagement.Application.Queries.GetVaccinationCard;
using VaccinationManagement.Domain.DTO;
using VaccinationManagement.Domain.Entity;

namespace VaccinationManagement.UnitTests.Api.Controller
{
    public class PersonControllerTest
    {
        private readonly Mock<IMediator> MediatorMock;
        private readonly PersonController Controller;

        public PersonControllerTest()
        {
            MediatorMock = new Mock<IMediator>();
            Controller = new PersonController(MediatorMock.Object);
        }

        [Fact(DisplayName = "Should return success true when person is created")]
        public async Task CreatePersonShouldReturnSuccess()
        {
            // Arrange
            var request = new CreatePersonRequest
            {
                Cpf = "12345678901",
                Name = "Maria da Silva"
            };

            var expectedResponse = new CreatePersonResponse
            {
                Success = true,
                Message = "Person created successfully.",
                Cpf = "12345678901",
                Name = "Maria da Silva"
            };

            MediatorMock
                .Setup(m => m.Send(It.IsAny<CreatePersonRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await Controller.CreatePerson(request, CancellationToken.None);

            // Assert
            var okResult = result as OkObjectResult;
            var responseData = okResult!.Value.Should().BeOfType<CreatePersonResponse>().Subject;

            responseData.Should().NotBeNull();
            responseData.Message.Should().Be("Person created successfully.");
            responseData.Success.Should().Be(true);
        }

        [Fact(DisplayName = "Should return success when delete a person")]
        public async Task DeletePersonShouldReturnSuccess()
        {
            // Arrange
            var request = new DeletePersonRequest
            {
                Cpf = "12345678901"
            };

            var expectedResponse = new DeletePersonResponse
            {
                Success = true,
                Message = "Person deleted successfully.",
                Cpf = "12345678901"
            };

            MediatorMock
                .Setup(m => m.Send(It.IsAny<DeletePersonRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await Controller.DeletePerson("12345678901", CancellationToken.None);

            // Assert
            var okResult = result as OkObjectResult;
            var responseData = okResult!.Value.Should().BeOfType<DeletePersonResponse>().Subject;

            responseData.Should().NotBeNull();
            responseData.Message.Should().Be("Person deleted successfully.");
            responseData.Success.Should().Be(true);
        }

        [Fact(DisplayName = "Should return success when retrieving all people")]
        public async Task GetAllPeopleShouldReturnSuccess()
        {
            // Arrange
            var request = new GetAllPeopleRequest();

            var expectedResponse = new GetAllPeopleResponse
            {
                Success = true,
                Message = "People retrieved successfully.",
                People = new List<Domain.DTO.PersonDto>
                {
                    new Domain.DTO.PersonDto
                    {
                        Cpf = "12345678901",
                        Name = "Maria da Silva",
                        Vaccinations = new List<Domain.DTO.VaccinationDto>
                        {
                            new Domain.DTO.VaccinationDto
                            {
                                VaccinationId = 1,
                                VaccineId = 1,
                                VaccineName = "COVID-19",
                                Dose = 1,
                                ApplicationDate = "01-01-2022"
                            }
                        }
                    }
                }
            };

            MediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllPeopleRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await Controller.GetAllPeople(request, CancellationToken.None);

            // Assert
            var okResult = result as OkObjectResult;
            var responseData = okResult!.Value.Should().BeOfType<GetAllPeopleResponse>().Subject;

            responseData.Should().NotBeNull();
            responseData.Message.Should().Be("People retrieved successfully.");
            responseData.Success.Should().Be(true);
        }

        [Fact(DisplayName = "Should return success when retrieving vaccination card by cpf")]
        public async Task GetVaccinationCardShouldReturnSuccess()
        {
            // Arrange
            var request = "12345678901";

            var expectedResponse = new GetVaccinationCardResponse
            {
                Success = true,
                Message = "Vaccination Card retrieved successfully.",
                Person = new PersonDto
                {
                    Cpf = "12345678901",
                    Name = "Maria da Silva",
                    Vaccinations = new List<VaccinationDto>
                    {
                        new VaccinationDto
                        {
                            VaccinationId = 1,
                            VaccineId = 1,
                            VaccineName = "COVID-19",
                            Dose = 1,
                            ApplicationDate = "01-01-2022"
                        }
                    }
                }
            };

            MediatorMock
                .Setup(m => m.Send(It.IsAny<GetVaccinationCardRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await Controller.GetVaccinationCard(request, CancellationToken.None);

            // Assert
            var okResult = result as OkObjectResult;
            var responseData = okResult!.Value.Should().BeOfType<GetVaccinationCardResponse>().Subject;

            responseData.Should().NotBeNull();
            responseData.Message.Should().Be("Vaccination Card retrieved successfully.");
            responseData.Success.Should().Be(true);
        }
    }
}