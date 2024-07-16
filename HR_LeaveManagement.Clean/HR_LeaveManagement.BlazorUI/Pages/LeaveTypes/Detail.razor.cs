using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR_LeaveManagement.BlazorUI.Pages.LeaveTypes
{
    public partial class Detail
    {
        [Inject]
        ILeaveTypeService leaveTypeService { get; set; }

        [Parameter]
        public int id { get; set; }

        LeaveTypeVM leaveType = new LeaveTypeVM();

        protected async override Task OnParametersSetAsync()
        {
            leaveType = await leaveTypeService.GetLeaveTypeById(id);
        }
    }
}