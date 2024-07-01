

using HR_LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class LeaveRequestDetailDTO
{
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public LeaveTypeDTO? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public DateTime RequestedDate { get; set; }
    public string? RequestComments { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateModified { get; set; }

    public string RequestingEmployeeId { get; set; } = string.Empty;
}
