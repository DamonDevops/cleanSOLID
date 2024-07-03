using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Services.Base;

namespace HR_LeaveManagement.BlazorUI.Services;

public class LeaveTypeService : BaseHttpService, ILeaveTypeService
{
    public LeaveTypeService(IClient client) : base(client)
    {
    }
}
