using FluentValidation;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(lt => lt.Id)
            .NotNull()
            .MustAsync(LeaveTypeMustExist);

        RuleFor(lt => lt.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters long");

        RuleFor(lt => lt.DefaultDays)
            .LessThan(100).WithMessage("{PropertyName} can't exceed 100")
            .GreaterThan(1).WithMessage("{PropertyName} can't be less than 1");

        RuleFor(lt => lt)
            .MustAsync(LeaveTypeNameUnique).WithMessage("LeaveType already exist");
    }

    private async Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command, CancellationToken token)
    {
        return await _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        var existing = await _leaveTypeRepository.GetByIdAsync(id);
        return existing != null;
    }
}
