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

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<ChangeLeaveRequestApprovalCommandHandler> _logger;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public ChangeLeaveRequestApprovalCommandHandler(IMapper mapper, IEmailSender emailSender, IAppLogger<ChangeLeaveRequestApprovalCommandHandler> logger,
        ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository, ILeaveAllocationRepository leaveAllocationRepository)
    {
        _mapper = mapper;
        _emailSender = emailSender;
        _logger = logger;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
    }

    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
        if(leaveRequest == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        leaveRequest.Approved = request.Approved;
        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        if (request.Approved)
        {
            int daysRequested = (int)(leaveRequest.EndingDate - leaveRequest.StartingDate).TotalDays;
            var allocation = await _leaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
            allocation.NumberOfDays -= daysRequested;

            await _leaveAllocationRepository.UpdateAsync(allocation);
        }


        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"The Approval status of your Leave Request from {leaveRequest.StartingDate:D} to {leaveRequest.EndingDate:D} has been updated",
                Subject = "Leave Request Approval Status Update"
            };

            await _emailSender.SendEmail(email);

        }catch(Exception e) 
        {
            _logger.LogWarning(e.Message);
        }

        return Unit.Value;
    }
}
