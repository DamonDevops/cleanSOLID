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

namespace HR_LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class LeaveAllocationDetailsQueryHandler : IRequestHandler<LeaveAllocationDetailsQuery, LeaveAllocationDetailDTO>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    public LeaveAllocationDetailsQueryHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository, IAppLogger<LeaveAllocationDetailsQueryHandler> logger)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
    }

    public async Task<LeaveAllocationDetailDTO> Handle(LeaveAllocationDetailsQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocationDetail = await _leaveAllocationRepository.GetByIdAsync(request.id);
        if (leaveAllocationDetail == null)
        {
            throw new NotFoundException(nameof(LeaveAllocation), request.id);
        }

        var data = _mapper.Map<LeaveAllocationDetailDTO>(leaveAllocationDetail);

        return data;
    }
}
