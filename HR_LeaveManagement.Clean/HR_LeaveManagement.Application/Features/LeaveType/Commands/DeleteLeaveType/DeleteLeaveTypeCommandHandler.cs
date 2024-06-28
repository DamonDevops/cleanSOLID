using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR_LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<DeleteLeaveTypeCommandHandler> _logger;

    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IAppLogger<DeleteLeaveTypeCommandHandler> logger)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        //Get from DB 
        var toDelete = await _leaveTypeRepository.GetByIdAsync(request.Id);

        //Verify DTO exist
        if(toDelete == null)
        {
            _logger.LogWarning("Validation error in delete request at {0} - {1}",nameof(LeaveType), request.Id);
            throw new NotFoundException(nameof(LeaveType), request.Id);
        }

        //remove from DB
        await _leaveTypeRepository.DeleteAsync(toDelete);

        //Return record ID
        return Unit.Value;
    }
}
