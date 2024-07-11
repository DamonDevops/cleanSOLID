using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR_LeaveManagement.BlazorUI.Pages.LeaveTypes
{
    public partial class Create
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Inject]
        ILeaveTypeService leaveTypeService { get; set; }

        LeaveTypeVM leaveType = new LeaveTypeVM();
        public string Message { get; private set; }
        private async Task CreateLeaveType()
        {
            var response = await leaveTypeService.CreateLeaveType(leaveType);
            if (response.Success)
            {
                NavigationManager.NavigateTo("/leavetypes/");
            }
            Message = response.Message;
        }
    }
}