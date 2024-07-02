using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Email;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using HR_LeaveManagement.Application.Models.EmailModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _logger;

    public UpdateLeaveRequestCommandHandler(IMapper mapper, IEmailSender emailSender, ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository,
        IAppLogger<UpdateLeaveRequestCommandHandler> logger)
    {
        _mapper = mapper;
        _emailSender = emailSender;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
        if(leaveRequest == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        var validator = new UpdateLeaveRequestCommandValidator(_leaveTypeRepository, _leaveRequestRepository);
        var validatorResults = await validator.ValidateAsync(request);

        if (validatorResults.Errors.Any())
        {
            _logger.LogWarning("Validation error in update request {0} - {1}", nameof(LeaveRequest), request.Id);
            throw new BadRequestException("Invalid LeaveRequest", validatorResults);
        }

        _mapper.Map(request, leaveRequest);
        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request for {leaveRequest.StartingDate:D} to {leaveRequest.EndingDate:D} has been updated successfully.",
                Subject = "Leave request Submitted"
            };

            await _emailSender.SendEmail(email);
        }
        catch(Exception e)
        {
            _logger.LogWarning(e.Message);
        }
        

        return Unit.Value;
    }
}
