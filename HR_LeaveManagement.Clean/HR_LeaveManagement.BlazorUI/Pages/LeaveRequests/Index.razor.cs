using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;

namespace HR_LeaveManagement.BlazorUI.Pages.LeaveRequests;

public partial class Index
{
    [Inject]
    NavigationManager NavigationManager { get; set; }
    [Inject]
    ILeaveRequestService LeaveRequestService { get; set; }

    public AdminLeaveRequestVM AdminLeaveRequestVM { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        AdminLeaveRequestVM = await LeaveRequestService.GetAdminLeaveRequests();
    }

    private void GoToDetail(int id)
    {
        NavigationManager.NavigateTo($"/leaverequests/detail/{id}");
    }
}