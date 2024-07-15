using AutoMapper;
using Blazored.LocalStorage;
using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Models.LeaveRequests;
using HR_LeaveManagement.BlazorUI.Services.Base;

namespace HR_LeaveManagement.BlazorUI.Services;

public class LeaveRequestService : BaseHttpService, ILeaveRequestService
{
    private readonly IMapper _mapper;

    public LeaveRequestService(IMapper mapper, IClient client, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task ApproveLeaveRequest(int id, bool approved)
    {
        try
        {
            var request = new ChangeLeaveRequestApprovalCommand{
                Id = id,
                Approved = approved
            };
            await _client.UpdateApprovalAsync(request);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<Response<Guid>> CreateLeaveRequest(LeaveRequestVM leaveRequestVM)
    {
        try
        {
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

    public async Task<AdminLeaveRequestVM> GetAdminLeaveRequests()
    {
        var leaveRequests = await _client.LeaveRequestAllAsync(isLoggedInUser: false);

        var model = new AdminLeaveRequestVM
        {
            TotalRequests = leaveRequests.Count,
            PendingRequests = leaveRequests.Count(q => q.Approved == null),
            ApprovedRequests = leaveRequests.Count(q => q.Approved == true),
            RejectedRequests = leaveRequests.Count(q => q.Approved == false),
            LeaveRequestVMs = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
        };
        return model;
    }

    public async Task<LeaveRequestVM> GetLeaveRequest(int id)
    {
        var leaveRequest = await _client.LeaveRequestGETAsync(id);
        return _mapper.Map<LeaveRequestVM>(leaveRequest);
    }

    public Task<EmployeeLeaveRequestVM> GetUserLeaveRequests()
    {
        throw new NotImplementedException();
    }
}