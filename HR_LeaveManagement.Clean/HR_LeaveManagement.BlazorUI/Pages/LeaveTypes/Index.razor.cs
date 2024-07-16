using Blazored.Toast.Services;
using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR_LeaveManagement.BlazorUI.Pages.LeaveTypes;

public partial class Index
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    [Inject]
    public ILeaveTypeService LeaveTypeService { get; set; }
    [Inject]
    public ILeaveAllocationService LeaveAllocationService { get; set; }
    [Inject]
    public IToastService ToastService { get; set; }

    public List<LeaveTypeVM> LeaveTypes { get; private set; }
    public string Message { get; set; } = string.Empty;

    protected void CreateLeaveType()
    {
        NavigationManager.NavigateTo("/leavetypes/create/");
    }
    protected void AllocateLeaveType(int id)
    {
        LeaveAllocationService.CreateLeaveAllocation(id);
    }
    protected void EditLeaveType(int id)
    {
        NavigationManager.NavigateTo($"/leavetypes/edit/{id}");
    }
    protected void DetailsLeaveType(int id)
    {
        NavigationManager.NavigateTo($"/leavetypes/details/{id}");
    }
    protected async Task DeleteLeaveType(int id)
    {
        var response = await LeaveTypeService.DeleteLeaveType(id);
        if (response.Success)
        {
            ToastService.ShowSuccess("Leave Type removed successfully");
            //StateHasChanged();
            await OnInitializedAsync();
        }
        else
        {
            Message = response.Message;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        LeaveTypes = await LeaveTypeService.GetLeavetypes();
    }
}