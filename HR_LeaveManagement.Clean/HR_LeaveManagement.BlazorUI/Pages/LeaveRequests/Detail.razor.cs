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

    LeaveRequestVM LeaveRequestVM = new LeaveRequestVM();

    public string ClassName = string.Empty;
    string HeadingName = string.Empty;

    protected async override Task OnParametersSetAsync()
    {
        LeaveRequestVM = await LeaveRequestService.GetLeaveRequest(id);
        Console.WriteLine($"{LeaveRequestVM.LeaveTypeId}");
        if (LeaveRequestVM.Approved == null)
        {
            ClassName = "warning";
            HeadingName = "Pending Approval";
            Console.WriteLine($"OK FOR: {ClassName} & {HeadingName}");
        }
        else if (LeaveRequestVM.Approved == true)
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
    //protected override async Task OnInitializedAsync()
    //{
        
    //}

    private async Task ChangeApproval(bool approvalStatus)
    {
        await LeaveRequestService.ApproveLeaveRequest(id, approvalStatus);
        NavigationManager.NavigateTo("/leaverequests/");
    }
}