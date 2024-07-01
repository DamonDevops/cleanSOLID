

using MediatR;

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public record GetLeaveRequestDetailsQuery : IRequest<LeaveRequestDetailDTO>
{
    public int Id { get; set; }
}
