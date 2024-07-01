
using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailDTO>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public GetLeaveRequestDetailsQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository, IAppLogger<GetLeaveRequestDetailsQueryHandler> logger)
    {
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
    }

    public async Task<LeaveRequestDetailDTO> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
    {
        var leaveRequestDetail = await _leaveRequestRepository.GetByIdAsync(request.id);
        if(leaveRequestDetail == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.id);
        }

        var data = _mapper.Map<LeaveRequestDetailDTO>(leaveRequestDetail);
        return data;
    }
}
