using AutoMapper;
using FluentValidation.Internal;
using HR_LeaveManagement.Application.Contracts.Email;
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

    public CreateLeaveRequestCommandHandler(IMapper mapper, IEmailSender emailSender, IAppLogger<CreateLeaveRequestCommandHandler> logger,
        ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository)
    {
        _mapper = mapper;
        _emailSender = emailSender;
        _logger = logger;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
    }

    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
        var validatorResults = await validator.ValidateAsync(request);

        if (validatorResults.Errors.Any())
        {
            throw new BadRequestException("Invalid LeaveRequest", validatorResults);
        }

        var toCreate = _mapper.Map<Domain.LeaveRequest>(request);
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
