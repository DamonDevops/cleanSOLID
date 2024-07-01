using FluentValidation;
using HR_LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Commands.Shared;

public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequest>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(q => q.StartingDate)
            .LessThan(q => q.EndingDate)
            .WithMessage("{PropertyName} must be before {ComparisonValue}");
        RuleFor(q => q.EndingDate)
            .GreaterThan(q => q.StartingDate)
            .WithMessage("{PropertyName} must be after {ComparisonValue}");
        RuleFor(q => q.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist");
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        var existing = _leaveTypeRepository.GetByIdAsync(id);
        return existing != null;
    }
}
