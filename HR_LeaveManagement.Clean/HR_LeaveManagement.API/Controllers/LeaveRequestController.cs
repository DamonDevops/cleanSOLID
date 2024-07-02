using HR_LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using HR_LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using HR_LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR_LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using HR_LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR_LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;
using HR_LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR_LeaveManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveRequestController : ControllerBase
{
    public IMediator _mediator;

    public LeaveRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<List<LeaveRequestDTO>> Get()
    {
        var leaveRequests = await _mediator.Send(new GetLeaveRequestsQuery());
        return leaveRequests;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveRequestDetailDTO>> GetById(int id)
    {
        return Ok(await _mediator.Send(new GetLeaveRequestDetailsQuery { Id = id }));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateLeaveRequestCommand createLeaveRequest)
    {
        var response = await _mediator.Send(createLeaveRequest);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveRequestCommand updateLeaveRequest)
    {
        await _mediator.Send(updateLeaveRequest);
        return NoContent();
    }

    [HttpPut]
    [Route("CancelRequest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(CancelLeaveRequestCommand cancelLeaveRequest)
    {
        await _mediator.Send(cancelLeaveRequest);
        return NoContent();
    }

    [HttpPut]
    [Route("UpdateApproval")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(ChangeLeaveRequestApprovalCommand changeApprovalLeaveRequest)
    {
        await _mediator.Send(changeApprovalLeaveRequest);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveRequestCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
