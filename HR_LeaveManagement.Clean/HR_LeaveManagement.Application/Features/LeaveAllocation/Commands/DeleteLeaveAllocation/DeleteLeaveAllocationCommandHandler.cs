using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IAppLogger<DeleteLeaveAllocationCommandHandler> _logger;

    public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IAppLogger<DeleteLeaveAllocationCommandHandler> logger)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var toDelete = await _leaveAllocationRepository.GetByIdAsync(request.Id);

        if(toDelete == null)
        {
            _logger.LogWarning("Validation error in delete request at {0} - {1}", nameof(LeaveAllocation), request.Id);
            throw new NotFoundException("Invalid LeaveAllocation", request.Id);
        }

        await _leaveAllocationRepository.DeleteAsync(toDelete);

        return Unit.Value;
    }
}
