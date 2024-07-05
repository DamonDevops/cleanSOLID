using AutoMapper;
using HR_LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR_LeaveManagement.BlazorUI.Services.Base;

namespace HR_LeaveManagement.BlazorUI.MappingProfiles;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<LeaveTypeDTO, LeaveTypeVM>().ReverseMap();
        CreateMap<CreateLeaveTypeCommand, LeaveTypeVM>();
        CreateMap<UpdateLeaveTypeCommand, LeaveTypeVM>();
    }
}
