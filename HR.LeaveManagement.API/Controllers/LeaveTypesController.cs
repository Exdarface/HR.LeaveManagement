using HR.LeaveManagement.Application.DTOs.LeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // GET: api/<LeaveTypesController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveTypeDTO>>> GetAll()
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypeListQuery());
            return Ok(leaveTypes);
        }

        // GET api/<LeaveTypesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveTypeDTO>> GetById(Guid id)
        {
            var leaveType = await _mediator.Send(new GetLeaveTypeDetailsQuery() { Id = id });
            return Ok(leaveType);
        }

        // POST api/<LeaveTypesController>
        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] CreateLeaveTypeDTO leaveType)
        {
            var createLeaveTypeCommand = new CreateLeaveTypeCommand() { LeaveType = leaveType };
            var response = await _mediator.Send(createLeaveTypeCommand);
            return Ok(response);
        }

        // PUT api/<LeaveTypesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveTypeDTO leaveType)
        {
            var updateLeaveTypeCommand = new UpdateLeaveTypeCommand() { LeaveType = leaveType };
            await _mediator.Send(updateLeaveTypeCommand);
            return NoContent();
        }

        // DELETE api/<LeaveTypesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteLeaveTypeCommand = new DeleteLeaveTypeCommand() { Id = id };
            await _mediator.Send(deleteLeaveTypeCommand);
            return NoContent();
        }
    }
}
