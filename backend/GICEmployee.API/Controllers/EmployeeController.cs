using GICEmployee.Application.Features.Cafe.Queries;
using GICEmployee.Application.Features.Employee.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GICEmployee.API.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IMediator mediator, ILogger<EmployeeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Created(string.Empty, value: new { id = result.Id });
        }

        [HttpGet]
        [Route("getbycafe")]
        public async Task<IActionResult> GetByCafe([FromQuery] Guid? cafe, CancellationToken cancellationToken)
        {
            var query = new GetEmployeesByCafeQuery(cafe);
            var employees = await _mediator.Send(query, cancellationToken);
            return Ok(employees);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updatedEmployee = await _mediator.Send(request, cancellationToken);
                return Ok(updatedEmployee);
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
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            try
            {
                var command = new DeleteEmployeeCommand { Id = id };
                await _mediator.Send(command, cancellationToken);
                return NoContent();  // Return 204 No Content if deletion is successful
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
