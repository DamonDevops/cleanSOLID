
using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Identity;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailDTO>
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public GetLeaveRequestDetailsQueryHandler(IMapper mapper, IUserService userService, ILeaveRequestRepository leaveRequestRepository, IAppLogger<GetLeaveRequestDetailsQueryHandler> logger)
    {
        _mapper = mapper;
        _userService = userService;
        _leaveRequestRepository = leaveRequestRepository;
    }

    public async Task<LeaveRequestDetailDTO> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
    {
        var leaveRequest = _mapper.Map<LeaveRequestDetailDTO>(await _leaveRequestRepository.GetByIdAsync(request.Id));
        if (leaveRequest == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }
        leaveRequest.Employee = await _userService.GetEmployeeById(leaveRequest.RequestingEmployeeId);

        return leaveRequest;
    }
}
