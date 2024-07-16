using AutoMapper;
using Blazored.LocalStorage;
using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR_LeaveManagement.BlazorUI.Services.Base;

namespace HR_LeaveManagement.BlazorUI.Services;

public class LeaveTypeService : BaseHttpService, ILeaveTypeService
{
    private readonly IMapper _mapper;
    public LeaveTypeService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<List<LeaveTypeVM>> GetLeavetypes()
    {
        //await AddBearerToken();
        var leaveTypes = await _client.LeaveTypeAllAsync();
        return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
    }

    public async Task<LeaveTypeVM> GetLeaveTypeById(int id)
    {
        //await AddBearerToken();
        var leaveType = await _client.LeaveTypeGETAsync(id);
        return _mapper.Map<LeaveTypeVM>(leaveType);
    }

    public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveTypeVM)
    {
        try
        {
            //await AddBearerToken();
            var createLeaveTypeCommand = _mapper.Map<CreateLeaveTypeCommand>(leaveTypeVM);
            await _client.LeaveTypePOSTAsync(createLeaveTypeCommand);
            return new Response<Guid>() { Success = true };
        }
        catch(ApiException e)
        {
            return ConvertApiExceptions<Guid>(e);
        }
        
    }

    public async Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeVM leaveTypeVM)
    {
        try
        {
            //await AddBearerToken();
            var updateLeaveTypeCommand = _mapper.Map<UpdateLeaveTypeCommand>(leaveTypeVM);
            await _client.LeaveTypePUTAsync(id.ToString(), updateLeaveTypeCommand);
            return new Response<Guid>() { Success = true };
        }
        catch (ApiException e)
        {
            return ConvertApiExceptions<Guid>(e);
        }
    }

    public async Task<Response<Guid>> DeleteLeaveType(int id)
    {
        try
        {
            //await AddBearerToken();
            await _client.LeaveTypeDELETEAsync(id);
            return new Response<Guid>() { Success = true };
        }
        catch (ApiException e)
        {
            return ConvertApiExceptions<Guid>(e);
        }
    }
}
