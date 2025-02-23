using EmployeeMicroserviceAPI.Features.Employees.Commands;
using EmployeeMicroserviceAPI.Features.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMicroserviceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAllEmployees), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _mediator.Send(new GetAllEmployeesQuery());
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            var result = await _mediator.Send(command);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand { Id = id });
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
