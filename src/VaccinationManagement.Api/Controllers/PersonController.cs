using Microsoft.AspNetCore.Mvc;
using MediatR;
using VaccinationManagement.Application.Command.CreatePerson;
using Microsoft.AspNetCore.Http;

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

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            // aqui futuramente chamar mediator
            return Ok(new { Id = id, Name = "Fulano" });
        }

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

        #endregion
    }
}