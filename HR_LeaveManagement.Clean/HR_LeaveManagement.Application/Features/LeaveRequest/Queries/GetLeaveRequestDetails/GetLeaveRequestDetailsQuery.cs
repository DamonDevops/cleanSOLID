

using MediatR;

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public record GetLeaveRequestDetailsQuery(int id) : IRequest<LeaveRequestDetailDTO>;
