using HR_LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR_LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR_LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR_LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR_LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace HR_LeaveManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LeaveTypeController : ControllerBase
{
    public IMediator _mediator;

    public LeaveTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<List<LeaveTypeDTO>> Get()
    {

        var leaveTypes = await _mediator.Send(new GetLeaveTypesQuery());
        return leaveTypes;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveTypeDetailDTO>> GetById(int id)
    {
        return Ok(await _mediator.Send(new GetLeaveTypeDetailsQuery { Id = id }));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateLeaveTypeCommand createLeaveTypeCommand) 
    {
        var response = await _mediator.Send(createLeaveTypeCommand);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveTypeCommand updateLeaveTypeCommand)
    {
        await _mediator.Send(updateLeaveTypeCommand);
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
        var command = new DeleteLeaveTypeCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
