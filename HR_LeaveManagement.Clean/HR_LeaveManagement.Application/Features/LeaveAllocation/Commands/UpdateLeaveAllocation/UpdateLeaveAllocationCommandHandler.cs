using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<UpdateLeaveAllocationCommandHandler> _logger;

    public UpdateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository, IAppLogger<UpdateLeaveAllocationCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveAllocationCommandValidator(_leaveTypeRepository, _leaveAllocationRepository);
        var validatorResults = validator.Validate(request);

        if (validatorResults.Errors.Any())
        {
            _logger.LogWarning("Validation error in update request {0} - {1}", nameof(LeaveAllocation), request.Id);
            throw new BadRequestException("Invalid Leave Allocation request", validatorResults);
        }

        var toUpdate = await _leaveAllocationRepository.GetByIdAsync(request.Id);
        _mapper.Map(request, toUpdate);

        await _leaveAllocationRepository.UpdateAsync(toUpdate);
        return Unit.Value;
    }
}
