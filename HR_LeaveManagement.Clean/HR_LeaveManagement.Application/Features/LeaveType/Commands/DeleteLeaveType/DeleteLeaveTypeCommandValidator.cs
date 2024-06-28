using FluentValidation;
using HR_LeaveManagement.Application.Contracts.Persistence;

namespace HR_LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandValidator : AbstractValidator<DeleteLeaveTypeCommand>
{
    public ILeaveTypeRepository _leaveTypeRepository;
    public DeleteLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(lt => lt.Id)
            .NotNull()
            .MustAsync(LeaveTypeMustExist);
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        var existing = await _leaveTypeRepository.GetByIdAsync(id);
        return existing != null;
    }
}
