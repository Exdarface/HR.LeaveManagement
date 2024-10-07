using HR.LeaveManagement.Application.DTOs.LeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        // GET: api/<LeaveAllocationsController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDTO>>> GetAll()
        {
            var leaveAllocations = await _mediator.Send(new GetLeaveAllocationListQuery());
            return Ok(leaveAllocations);
        }

        // GET api/<LeaveAllocationsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDTO>> GetById(Guid id)
        {
            var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailsQuery() { Id = id });
            return Ok(leaveAllocation);
        }

        // POST api/<LeaveAllocationsController>
        [HttpPost]
        public async Task<ActionResult<LeaveAllocationDTO>> Post([FromBody] CreateLeaveAllocationDTO leaveAllocation)
        {
            var response = await _mediator.Send(new CreateLeaveAllocationCommand() { LeaveAllocation = leaveAllocation });
            return Ok(response);
        }

        // PUT api/<LeaveAllocationsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationDTO leaveAllocation)
        {
            await _mediator.Send(new UpdateLeaveAllocationCommand() { LeaveAllocation = leaveAllocation });
            return NoContent();
        }

        // DELETE api/<LeaveAllocationsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteLeaveAllocationCommand() { Id = id });
            return NoContent();
        }
    }
}
