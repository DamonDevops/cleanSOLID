using FluentValidation;
using HR_LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public UpdateLeaveRequestCommandValidator(ILeaveRequestRepository leaveRequestRepository)
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
