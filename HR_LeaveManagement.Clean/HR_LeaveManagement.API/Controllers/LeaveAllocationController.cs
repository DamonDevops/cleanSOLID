using HR_LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR_LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR_LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR_LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using HR_LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR_LeaveManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveAllocationController : ControllerBase
{
    public IMediator _mediator;

    public LeaveAllocationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDTO>>> Get(bool IsLoggedInUser = false)
    {
        var leaveAllocations = await _mediator.Send(new GetLeaveAllocationsQuery());
        return Ok(leaveAllocations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveAllocationDetailDTO>> GetById(int id)
    {
        return Ok(await _mediator.Send(new LeaveAllocationDetailsQuery(id)));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateLeaveAllocationCommand createLeaveAllocationCommand)
    {
        var response = await _mediator.Send(createLeaveAllocationCommand);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveAllocationCommand updateLeaveAllocationCommand)
    {
        await _mediator.Send(updateLeaveAllocationCommand);
        return NoContent();
    }

    // DELETE api/<LeaveTypeController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveAllocationCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
