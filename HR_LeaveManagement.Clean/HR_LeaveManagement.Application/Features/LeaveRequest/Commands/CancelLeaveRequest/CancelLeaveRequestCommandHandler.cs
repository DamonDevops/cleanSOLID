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

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<CancelLeaveRequestCommandHandler> _logger;

    public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender, IAppLogger<CancelLeaveRequestCommandHandler> logger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var toCancel = await _leaveRequestRepository.GetByIdAsync(request.Id);
        if(toCancel == null)
        {
            throw new NotFoundException(nameof(LeaveRequest),request.Id);
        }

        toCancel.Cancelled = true;

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your Leave Request from {toCancel.StartingDate:D} to {toCancel.EndingDate:D} has been cancelled successfully",
                Subject = "Leave Request Cancelled"
            };

            await _emailSender.SendEmail(email);

        }catch(Exception e)
        {
            _logger.LogWarning(e.Message);
        }
        return Unit.Value;
    }
}
