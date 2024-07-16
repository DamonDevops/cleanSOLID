using Blazored.LocalStorage;
using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Services.Base;

namespace HR_LeaveManagement.BlazorUI.Services;

public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
{
    public LeaveAllocationService(IClient client, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
    }

    public async Task<Response<Guid>> CreateLeaveAllocation(int leaveTypeId)
    {
        try
        {
            var response = new Response<Guid>();
            CreateLeaveAllocationCommand command = new()
            {
                LeaveTypeId = leaveTypeId
            };

            await _client.LeaveAllocationPOSTAsync(command);
            return response;
        }
        catch(ApiException e)
        {
            return ConvertApiExceptions<Guid>(e);
        }
    }
}