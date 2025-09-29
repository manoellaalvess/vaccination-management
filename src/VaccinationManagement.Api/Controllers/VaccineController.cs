using Microsoft.AspNetCore.Mvc;
using MediatR;
using VaccinationManagement.Application.Command.CreateVaccine;
using VaccinationManagement.Application.Queries.GetAllVaccine;

namespace VaccinationManagement.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class VaccineController : ControllerBase
    {
        #region Properties

        private readonly IMediator Mediator;

        #endregion

        #region Constructor

        public VaccineController(IMediator mediator)
        {
            Mediator = mediator;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Creates new Vaccine
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// {
        ///   "vaccineName": "H1N1"
        /// }
        /// </remarks>
        [HttpPost]
        [Route("/CreateVaccine")]
        [ProducesResponseType(typeof(CreateVaccineResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateVaccine([FromBody] CreateVaccineRequest request, CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Gets all Vaccines
        /// </summary>
        /// <remarks>
        [HttpGet]
        [Route("/GetAllVaccine")]
        [ProducesResponseType(typeof(IEnumerable<GetAllVaccineResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetAllVaccines(CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(new GetAllVaccineRequest(), cancellationToken);

            return Ok(response);
        }

        #endregion

    }
}