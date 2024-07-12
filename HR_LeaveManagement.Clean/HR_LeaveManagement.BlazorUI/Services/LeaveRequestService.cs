using Blazored.LocalStorage;
using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Models.LeaveRequests;
using HR_LeaveManagement.BlazorUI.Services.Base;

namespace HR_LeaveManagement.BlazorUI.Services;

public class LeaveRequestService : BaseHttpService, ILeaveRequestService
{
    public LeaveRequestService(IClient client, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
    }

    public Task ApproveLeaveRequest(int id, bool approved)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<Guid>> CreateLeaveRequest(LeaveRequestVM leaveRequestVM)
    {
        try
        {
            await AddBearerToken();
            var response = new Response<Guid>();
            CreateLeaveRequestCommand command = new()
            {
                LeaveTypeId = leaveRequestVM.LeaveTypeId,
                StartingDate = (DateTime)leaveRequestVM.StartingDate,
                EndingDate = (DateTime)leaveRequestVM.EndingDate,
                RequestComments = leaveRequestVM.RequestComments
            };

            await _client.LeaveRequestPOSTAsync(command);
            return response;
        }
        catch (ApiException e)
        {
            return ConvertApiExceptions<Guid>(e);
        }
    }

    public Task DeleteLeaveRequest(int id)
    {
        throw new NotImplementedException();
    }

    public Task<AdminLeaveRequestVM> GetAdminLeaveRequests()
    {
        throw new NotImplementedException();
    }

    public Task<LeaveRequestVM> GetLeaveRequest(int id)
    {
        throw new NotImplementedException();
    }

    public Task<EmployeeLeaveRequestVM> GetUserLeaveRequests()
    {
        throw new NotImplementedException();
    }
}