using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Identity;
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
    private readonly IUserService _userService;
    private readonly IAppLogger<GetLeaveRequestsQueryHandler> _logger;

    public GetLeaveRequestsQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository, IUserService userService, IAppLogger<GetLeaveRequestsQueryHandler> logger)
    { 
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
        _userService = userService;
        _logger = logger;
    }

    public async Task<List<LeaveRequestDTO>> Handle(GetLeaveRequestsQuery request, CancellationToken cancellationToken)
    {
        var data = new List<LeaveRequestDTO>();
        if (request.IsLoggedInUser)
        {
            var userId = _userService.UserId;
            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails(userId);

            var employee = await _userService.GetEmployeeById(userId);
            data = _mapper.Map<List<LeaveRequestDTO>>(leaveRequests);
            foreach(var item in data)
            {
                item.Employee = employee;
            }
        }
        else
        {
            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
            data = _mapper.Map<List<LeaveRequestDTO>>(leaveRequests);
            foreach(var item in data)
            {
                item.Employee = await _userService.GetEmployeeById(item.RequestingEmployeeId);
            }
        }

        _logger.LogInformation("List of LeaveTypes was retrieved successfully");
        return data;
    }
}
