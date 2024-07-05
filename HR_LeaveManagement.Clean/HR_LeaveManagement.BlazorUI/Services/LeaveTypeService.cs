using AutoMapper;
using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR_LeaveManagement.BlazorUI.Services.Base;

namespace HR_LeaveManagement.BlazorUI.Services;

public class LeaveTypeService : BaseHttpService, ILeaveTypeService
{
    private readonly IMapper _mapper;
    public LeaveTypeService(IClient client, IMapper mapper) : base(client)
    {
        _mapper = mapper;
    }

    public async Task<List<LeaveTypeVM>> GetLeavetypes()
    {
        var leaveTypes = await _client.LeaveTypeAllAsync();
        return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
    }

    public async Task<LeaveTypeVM> GetLeaveTypeById(int id)
    {
        var leaveType = await _client.LeaveTypeGETAsync(id);
        return _mapper.Map<LeaveTypeVM>(leaveType);
    }

    public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveTypeVM)
    {
        try
        {
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
            await _client.LeaveTypeDELETEAsync(id);
            return new Response<Guid>() { Success = true };
        }
        catch (ApiException e)
        {
            return ConvertApiExceptions<Guid>(e);
        }
    }
}
