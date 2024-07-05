using HR_LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR_LeaveManagement.BlazorUI.Services.Base;

namespace HR_LeaveManagement.BlazorUI.Contracts;

public interface ILeaveTypeService
{
    Task<List<LeaveTypeVM>> GetLeavetypes();
    Task<LeaveTypeVM> GetLeaveTypeById(int id);
    Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveTypeVM);
    Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeVM leaveTypeVM);
    Task<Response<Guid>> DeleteLeaveType(int id);
}
