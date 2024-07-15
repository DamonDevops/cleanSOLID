using HR_LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR_LeaveManagement.Application.Models.IdentityModels;

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public class LeaveRequestDTO
{
    public int Id { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public LeaveTypeDTO? LeaveType { get; set; }
    public DateTime RequestedDate { get; set; }
    public bool? Approved { get; set; }
    public bool? Cancelled { get; set; }

    public Employee Employee { get; set; }
    public string RequestingEmployeeId { get; set; } = string.Empty;
}
