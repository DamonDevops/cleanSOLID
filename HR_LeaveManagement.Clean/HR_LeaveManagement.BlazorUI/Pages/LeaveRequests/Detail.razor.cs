using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;

namespace HR_LeaveManagement.BlazorUI.Pages.LeaveRequests;

public partial class Detail
{
    [Inject]
    ILeaveRequestService LeaveRequestService { get; set; }
    [Inject]
    NavigationManager NavigationManager { get; set; }

    [Parameter]
    public int id { get; set; }

    string ClassName = string.Empty;
    string HeadingName = string.Empty;

    public LeaveRequestVM LeaveRequestVM { get; set; } = new LeaveRequestVM();

    protected override async Task OnParametersSetAsync()
    {
        LeaveRequestVM = await LeaveRequestService.GetLeaveRequest(id);
    }
    protected override async Task OnInitializedAsync()
    {
        if(LeaveRequestVM.Approved == null)
        {
            ClassName = "warning";
            HeadingName = "Pending Approval";
        }
        else if(LeaveRequestVM.Approved == true)
        {
            ClassName = "success";
            HeadingName = "Approved";
        }
        else
        {
            ClassName = "danger";
            HeadingName = "Rejected";
        }
    }

    private async Task ChangeApproval(bool approvalStatus)
    {
        await LeaveRequestService.ApproveLeaveRequest(id, approvalStatus);
        NavigationManager.NavigateTo("/leaverequests/");
    }
}