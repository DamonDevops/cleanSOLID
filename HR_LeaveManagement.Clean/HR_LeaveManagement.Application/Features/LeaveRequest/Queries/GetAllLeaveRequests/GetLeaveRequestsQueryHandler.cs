using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public class GetLeaveRequestsQueryHandler : IRequestHandler<GetLeaveRequestsQuery, List<LeaveRequestDTO>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IAppLogger<GetLeaveRequestsQueryHandler> _logger;

    public GetLeaveRequestsQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository, IAppLogger<GetLeaveRequestsQueryHandler> logger)
    {
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
        _logger = logger;
    }

    public async Task<List<LeaveRequestDTO>> Handle(GetLeaveRequestsQuery request, CancellationToken cancellationToken)
    {
        var leaveRequests = await _leaveRequestRepository.GetAsync();

        var data = _mapper.Map<List<LeaveRequestDTO>>(leaveRequests);

        _logger.LogInformation("List of LeaveTypes was retrieved successfully");
        return data;
    }
}
