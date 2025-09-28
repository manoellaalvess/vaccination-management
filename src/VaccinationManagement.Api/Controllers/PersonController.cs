using Microsoft.AspNetCore.Mvc;
using MediatR;
using VaccinationManagement.Application.Command.CreatePerson;
using VaccinationManagement.Application.Command.DeletePerson;
using VaccinationManagement.Application.Queries.GetAllPeople;
using VaccinationManagement.Application.Queries.GetByCpf;

namespace VaccinationManagement.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        #region Properties

        private readonly IMediator Mediator;

        #endregion

        #region Constructor

        public PersonController(IMediator mediator)
        {
            Mediator = mediator;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Creates new Person
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// {
        ///   "cpf": "11122233344",
        ///   "name": "Fulano de Tal"
        /// }
        /// </remarks>
        [HttpPost]
        [Route("/CreatePerson")]
        [ProducesResponseType(typeof(CreatePersonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonRequest request, CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(request);

            return Ok(new { message = "Person created successfully.", data = response });
        }

        /// <summary>
        /// Delete a Person
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(DeletePersonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> DeletePerson(string id, CancellationToken cancellationToken)
        {
            var request = new DeletePersonRequest { Cpf = id };

            var response = await Mediator.Send(request);

            return Ok(new { message = "Person removed successfully.", data = response });
        }

        /// <summary>
        /// Get all people
        /// </summary>
        [HttpGet]
        [Route("GetAllPeople")]
        [ProducesResponseType(typeof(GetAllPeopleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetAllPeople([FromRoute] GetAllPeopleRequest request, CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(request, cancellationToken);

            return Ok(new { message = response.People.Any() ? "People retrieved successfully." : "No person found.", data = response });
        }

        /// <summary>
        /// Get person by cpf
        /// </summary>
        [HttpGet]
        [Route("{cpf}")]
        [ProducesResponseType(typeof(GetByCpfResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetByCpf(string cpf, CancellationToken cancellationToken)
        {
            var request = new GetByCpfRequest { Cpf = cpf };

            var response = await Mediator.Send(request, cancellationToken);

            return Ok(new { message = "Person retrieved successfully.", data = response });
        }

        #endregion
    }
}