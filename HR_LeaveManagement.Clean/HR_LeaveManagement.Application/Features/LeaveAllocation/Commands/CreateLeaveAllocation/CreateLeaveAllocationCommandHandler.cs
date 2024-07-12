using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Identity;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR_LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IUserService _userService;

    public CreateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository, IUserService userService)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _userService = userService;
    }

    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
        var validatorResults = await validator.ValidateAsync(request);

        if (validatorResults.Errors.Any())
        {
            throw new BadRequestException("Invalid Leave Allocation request", validatorResults);
        }

        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        var employees = await _userService.GetEmployees();

        var period = DateTime.UtcNow.Year;
        var allocations = new List<Domain.LeaveAllocation>();

        foreach (var employee in employees)
        {
            var alreadyAllocated = await _leaveAllocationRepository.AllocationExists(employee.Id, request.LeaveTypeId, period);
            if (!alreadyAllocated)
            {
                allocations.Add(new Domain.LeaveAllocation
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveType.Id,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = period
                });
            }
        }

        if (allocations.Any())
        {
            await _leaveAllocationRepository.AddAllocations(allocations);
        }

        //var toCreate = _mapper.Map<Domain.LeaveAllocation>(request);
        //await _leaveAllocationRepository.CreateAsync(toCreate);

        return Unit.Value;
    }
}
