using Microsoft.AspNetCore.Mvc;
using MediatR;
using VaccinationManagement.Application.Command.AddVaccination;

namespace VaccinationManagement.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class VaccinationController : ControllerBase
    {
        #region Properties

        private readonly IMediator Mediator;

        #endregion

        #region Constructor

        public VaccinationController(IMediator mediator)
        {
            Mediator = mediator;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Creates new vaccine record
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// {
        ///   "vaccineId": "1223",
        ///   "personCpf": "12345678901",
        ///   "dose": 1
        /// }
        /// </remarks>
        [HttpPost]
        [Route("/AddVaccination")]
        [ProducesResponseType(typeof(AddVaccinationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateVaccination([FromBody] AddVaccinationRequest request, CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(request, cancellationToken);

            return Ok(new { message = $"Vaccination {response} created successfully.", data = response });
        }

        #endregion
    }
}