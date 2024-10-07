using HR.LeaveManagement.Application.DTOs.LeaveRequests;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // GET: api/<LeaveRequestController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestDTO>>> GetAll()
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestListQuery());
            return Ok(leaveRequests);
        }

        // GET api/<LeaveRequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDTO>> GetById(Guid id)
        {
            var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailsQuery() { Id = id });
            return Ok(leaveRequest);
        }

        // POST api/<LeaveRequestController>
        [HttpPost]
        public async Task<ActionResult<LeaveRequestDTO>> Post([FromBody] CreateLeaveRequestDTO leaveRequest)
        {
            var response = await _mediator.Send(new CreateLeaveRequestCommand() { LeaveRequest = leaveRequest });
            return Ok(response);
        }

        // PUT api/<LeaveRequestController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveRequestDTO leaveRequest)
        {
            await _mediator.Send(new UpdateLeaveRequestCommand() { LeaveRequest = leaveRequest });
            return NoContent();
        }

        // PUT api/<LeaveRequestController>/approve
        [HttpPut("approve")]
        public async Task<ActionResult> Approve([FromBody] UpdateLeaveRequestCommand updateLeaveRequestCommand)
        {
            await _mediator.Send(new UpdateLeaveRequestCommand() { LeaveRequest = updateLeaveRequestCommand.LeaveRequest, ChangeLeaveRequestApproval = updateLeaveRequestCommand.ChangeLeaveRequestApproval });
            return NoContent();
        }

        // DELETE api/<LeaveRequestController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteLeaveRequestCommand() { Id = id });
            return NoContent();
        }
    }
}
