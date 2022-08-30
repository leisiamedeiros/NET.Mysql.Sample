using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET.Mysql.Sample.Application.UseCases.CreateContact;
using NET.Mysql.Sample.Application.UseCases.GetAllContatcs;
using NET.Mysql.Sample.WebApi.Transport;
using System.Threading;
using System.Threading.Tasks;

namespace NET.Mysql.Sample.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get All Contacts
        /// </summary>
        /// <param name="cancellation"></param>
        /// <response code="200">Success</response>
        /// <response code="500">Error</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllContactsOutput))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetAll(CancellationToken cancellation)
        {
            var contacts = await _mediator.Send(new GetAllContactsInput(), cancellation);

            return Ok(contacts);
        }

        /// <summary>
        /// Create a contact
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellation"></param>
        /// <response code="201">Created</response>
        /// <response code="500">Error</response>
        /// <response code="400">Bad Request</response>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Create([FromBody] CreateContactInput input, CancellationToken cancellation)
        {
            await _mediator.Send(input, cancellation);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
