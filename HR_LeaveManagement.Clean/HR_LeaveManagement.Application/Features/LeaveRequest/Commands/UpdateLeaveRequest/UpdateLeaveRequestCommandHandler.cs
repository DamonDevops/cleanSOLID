using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
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
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _logger;

    public UpdateLeaveRequestCommandHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository, IAppLogger<UpdateLeaveRequestCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveRequestCommandValidator(_leaveRequestRepository);
        var validatorResults = validator.Validate(request);

        if (validatorResults.Errors.Any())
        {
            _logger.LogWarning("Validation error in update request {0} - {1}", nameof(LeaveRequest), request.Id);
            throw new BadRequestException("Invalid LeaveRequest", validatorResults);
        }

        var toUpdate = _mapper.Map<Domain.LeaveRequest>(request);
        await _leaveRequestRepository.UpdateAsync(toUpdate);

        return Unit.Value;
    }
}
