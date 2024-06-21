using FluentValidation;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Domain;

namespace HR_LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(lt => lt.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters long");

            RuleFor(lt => lt.DefaultDays)
                .LessThan(100).WithMessage("{PropertyName} can't exceed 100")
                .GreaterThan(1).WithMessage("{PropertyName} can't be less than 1");

            RuleFor(lt => lt)
                .MustAsync(LeaveTypeNameUnique).WithMessage("LeaveType already exist");

            _leaveTypeRepository = leaveTypeRepository;
        }

        private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
        {
            return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
        }
    }
}
