using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;

public class GetLeaveAllocationsQueryHandler : IRequestHandler<GetLeaveAllocationsQuery, List<LeaveAllocationDTO>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IAppLogger<GetLeaveAllocationsQueryHandler> _logger;

    public GetLeaveAllocationsQueryHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository ,IAppLogger<GetLeaveAllocationsQueryHandler> logger)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
        _logger = logger;
    }

    async Task<List<LeaveAllocationDTO>> IRequestHandler<GetLeaveAllocationsQuery, List<LeaveAllocationDTO>>.Handle(GetLeaveAllocationsQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocations = await _leaveAllocationRepository.GetAllocationsWithDetails();

        var data = _mapper.Map<List<LeaveAllocationDTO>>(leaveAllocations);

        _logger.LogInformation("List of LeaveTypes was retrieved successfully");
        return data;
    }
}
