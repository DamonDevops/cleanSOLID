using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR_LeaveManagement.BlazorUI.Pages.LeaveTypes
{
    public partial class Edit
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Inject]
        ILeaveTypeService LeaveTypeService { get; set; }

        [Parameter]
        public int id { get; set; }
        public string Message { get; private set; }

        LeaveTypeVM leaveType = new LeaveTypeVM();

        protected async override Task OnParametersSetAsync()
        {
            leaveType = await LeaveTypeService.GetLeaveTypeById(id);
        }

        private async Task EditLeaveType()
        {
            var response = await LeaveTypeService.UpdateLeaveType(id, leaveType);
            if (response.Success)
            {
                NavigationManager.NavigateTo("/leavetypes/");
            }
            Message = response.Message;
        }
    }
}