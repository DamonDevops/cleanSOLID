using AutoMapper;
using FluentValidation.Internal;
using HR_LeaveManagement.Application.Contracts.Email;
using HR_LeaveManagement.Application.Contracts.Identity;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using HR_LeaveManagement.Application.Features.LeaveRequest.Commands.Shared;
using HR_LeaveManagement.Application.Models.EmailModels;
using MediatR;

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
{
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IUserService _userService;

    public CreateLeaveRequestCommandHandler(IMapper mapper, IEmailSender emailSender, IAppLogger<CreateLeaveRequestCommandHandler> logger,
        ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository, ILeaveAllocationRepository leaveAllocationRepository,
        IUserService userService)
    {
        _mapper = mapper;
        _emailSender = emailSender;
        _logger = logger;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
        _userService = userService;
    }

    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
        var validatorResults = await validator.ValidateAsync(request);

        if (validatorResults.Errors.Any())
        {
            throw new BadRequestException("Invalid LeaveRequest", validatorResults);
        }

        var employeeId = _userService.UserId;
        var allocation = await _leaveAllocationRepository.GetUserAllocations(employeeId, request.LeaveTypeId);
        if(allocation is null)
        {
            validatorResults.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.LeaveTypeId),"You don't have any allocation for this leave type"));
            throw new BadRequestException("Invalid LeaveRequest", validatorResults);
        }

        int daysRequested = (int)(request.EndingDate - request.StartingDate).TotalDays;
        if(daysRequested > allocation.NumberOfDays)
        {
            validatorResults.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.EndingDate), "You don't have enough days for this request"));
            throw new BadRequestException("Invalid LeaveRequest", validatorResults);
        }

        var toCreate = _mapper.Map<Domain.LeaveRequest>(request);
        toCreate.RequestingEmployeeId = employeeId;
        await _leaveRequestRepository.CreateAsync(toCreate);

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your Leave Request from {request.StartingDate:D} to {request.EndingDate:D} has been submitted successfully",
                Subject = "Leave Request Submitted"
            };

            await _emailSender.SendEmail(email);
        }catch(Exception e)
        {
            _logger.LogWarning(e.Message);
        }

        return Unit.Value;
    }
}
