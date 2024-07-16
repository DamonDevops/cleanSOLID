using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Models.LeaveRequests;
using HR_LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR_LeaveManagement.BlazorUI.Pages.LeaveRequests;

public partial class Create
{
    [Inject]
    ILeaveTypeService leaveTypeService { get; set; }
    [Inject]
    ILeaveRequestService LeaveRequestService { get; set; }
    [Inject]
    NavigationManager NavigationManager { get; set; }

    LeaveRequestVM LeaveRequest { get; set; } = new LeaveRequestVM();
    List<LeaveTypeVM> leaveTypes { get; set; } = new List<LeaveTypeVM>();

    protected override async Task OnInitializedAsync()
    {
        leaveTypes = await leaveTypeService.GetLeavetypes();
    }
    private async void HandleValidSubmit()
    {
        await LeaveRequestService.CreateLeaveRequest(LeaveRequest);
        NavigationManager.NavigateTo("/leaverequests/");
    }

}