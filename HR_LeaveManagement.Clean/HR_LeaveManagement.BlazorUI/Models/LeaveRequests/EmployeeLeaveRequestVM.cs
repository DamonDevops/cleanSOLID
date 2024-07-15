using HR.LeaveManagement.BlazorUI.Models.LeaveAllocations;

namespace HR_LeaveManagement.BlazorUI.Models.LeaveRequests;

public class EmployeeLeaveRequestVM
{
    public List<LeaveAllocationVM> LeaveAllocations { get; set; } = new List<LeaveAllocationVM>();
    public List<LeaveRequestVM> LeaveRequests { get; set; } = new List<LeaveRequestVM>();
}
