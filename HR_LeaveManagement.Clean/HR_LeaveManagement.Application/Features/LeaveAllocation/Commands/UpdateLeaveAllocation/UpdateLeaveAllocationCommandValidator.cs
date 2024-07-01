using FluentValidation;
using HR_LeaveManagement.Application.Contracts.Persistence;

namespace HR_LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator :AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;

        RuleFor(q => q.NumberOfDays)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than {ComparisonValue}");
        RuleFor(q => q.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year)
            .WithMessage("{PropertyName} must be greater after {ComparisonValue}");
        RuleFor(q => q.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist");
        RuleFor(q => q.Id)
            .NotNull()
            .MustAsync(LeaveAllocationMustExist)
            .WithMessage("{PropertyName} must be present");
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        var existing = await _leaveTypeRepository.GetByIdAsync(id);
        return existing != null;
    }
    private async Task<bool> LeaveAllocationMustExist(int id, CancellationToken token)
    {
        var existing = await _leaveAllocationRepository.GetByIdAsync(id);
        return existing != null;
    }
}
