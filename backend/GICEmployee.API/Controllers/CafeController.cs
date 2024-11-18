using GICEmployee.Application.Features.Cafe.Commands;
using GICEmployee.Application.Features.Cafe.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GICEmployee.API.Controllers
{
    [ApiController]
    [Route("api/cafe")]
    public class CafeController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly ILogger<CafeController> _logger;

        public CafeController(IMediator mediator, ILogger<CafeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCafeCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Created(string.Empty, value: new { id = result.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? location, CancellationToken cancellationToken)
        {
            var query = new GetCafesByLocationQuery(location);
            var cafes = await _mediator.Send(query, cancellationToken);
            return Ok(cafes);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCafeCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var updatedCafe = await _mediator.Send(command, cancellationToken);
                return Ok(updatedCafe);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var command = new DeleteCafeCommand { Id = id };
                await _mediator.Send(command, cancellationToken);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
