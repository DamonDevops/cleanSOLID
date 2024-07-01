using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IAppLogger<DeleteLeaveRequestCommandHandler> _logger;

    public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IAppLogger<DeleteLeaveRequestCommandHandler> logger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var toDelete = await _leaveRequestRepository.GetByIdAsync(request.Id);
        if(toDelete == null)
        {
            _logger.LogWarning("Validation error in delete request at {0} - {1}", nameof(LeaveRequest), request.Id);
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        await _leaveRequestRepository.DeleteAsync(toDelete);

        return Unit.Value;
    }
}
