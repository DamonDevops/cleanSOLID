using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace HR_LeaveManagement.BlazorUI.Pages.LeaveRequests;

public partial class EmployeeIndex : ComponentBase
{
    [Inject]
    NavigationManager NavigationManager { get; set; }
    [Inject]
    IJSRuntime js { get; set; }
    [Inject]
    ILeaveRequestService LeaveRequestService { get; set; }

    public EmployeeLeaveRequestVM EmployeeLeaveRequestVM { get; set; } = new();
    public string Message { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        EmployeeLeaveRequestVM = await LeaveRequestService.GetUserLeaveRequests();
    }

    async Task CancelRequestAsync(int id)
    {
        var confirm = await js.InvokeAsync<bool>("confirm", "Do you want to cancel this request?");
        if (confirm)
        {
            var response = await LeaveRequestService.CancelLeaveRequest(id);
            if (response.Success)
            {
                StateHasChanged();
            }
            else
            {
                Message = response.Message;
            }
        }
    }
}