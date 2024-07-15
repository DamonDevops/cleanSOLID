using AutoMapper;
using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Models.LeaveAllocations;
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

    public async Task<Response<Guid>> ApproveLeaveRequest(int id, bool approved)
    {
        try
        {
            var response = new Response<Guid>();
            var request = new ChangeLeaveRequestApprovalCommand{
                Id = id,
                Approved = approved
            };
            await _client.UpdateApprovalAsync(request);
            return response;
        }
        catch (ApiException e)
        {
            return ConvertApiExceptions<Guid>(e);
        }
    }

    public async Task<Response<Guid>> CancelLeaveRequest(int id)
    {
        try
        {
            var response = new Response<Guid>();
            var request = new CancelLeaveRequestCommand { Id = id };
            await _client.CancelRequestAsync(request);
            return response;
        }
        catch (ApiException e)
        {
            return ConvertApiExceptions<Guid>(e);
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

    public async Task<EmployeeLeaveRequestVM> GetUserLeaveRequests()
    {
        var leaveRequests = await _client.LeaveRequestAllAsync(isLoggedInUser: true);
        var allocations = await _client.LeaveAllocationAllAsync(isLoggedInUser: true);
        var model = new EmployeeLeaveRequestVM
        {
            LeaveAllocations = _mapper.Map<List<LeaveAllocationVM>>(allocations),
            LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
        };

        return model;
    }
}