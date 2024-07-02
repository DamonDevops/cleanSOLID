using FluentValidation;
using HR_LeaveManagement.Application.Contracts.Persistence;

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandValidator : AbstractValidator<DeleteLeaveRequestCommand>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public DeleteLeaveRequestCommandValidator(ILeaveRequestRepository leaveRequestRepository)
    {
        _leaveRequestRepository = leaveRequestRepository;

        RuleFor(q => q.Id)
            .NotNull()
            .MustAsync(LeaveRequestMustExist);
    }

    private async Task<bool> LeaveRequestMustExist(int id, CancellationToken token)
    {
        var existing = await _leaveRequestRepository.GetByIdAsync(id);
        return existing != null;
    }
}
