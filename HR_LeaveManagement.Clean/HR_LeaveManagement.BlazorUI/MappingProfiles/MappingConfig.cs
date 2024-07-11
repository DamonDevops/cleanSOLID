using AutoMapper;
using HR_LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR_LeaveManagement.BlazorUI.Services.Base;

namespace HR_LeaveManagement.BlazorUI.MappingProfiles;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<LeaveTypeDTO, LeaveTypeVM>().ReverseMap();
        CreateMap<LeaveTypeDetailDTO, LeaveTypeVM>().ReverseMap();
        CreateMap<CreateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();
        CreateMap<UpdateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();

        CreateMap<LeaveRequestDTO, LeaveTypeVM>().ReverseMap();
        CreateMap<LeaveRequestDetailDTO, LeaveTypeVM>().ReverseMap();
        CreateMap<CreateLeaveRequestCommand, LeaveTypeVM>().ReverseMap();
        CreateMap<UpdateLeaveRequestCommand, LeaveTypeVM>().ReverseMap();

        CreateMap<LeaveAllocationDTO, LeaveTypeVM>().ReverseMap();
        CreateMap<LeaveAllocationDetailDTO, LeaveTypeVM>().ReverseMap();
        CreateMap<CreateLeaveAllocationCommand, LeaveTypeVM>().ReverseMap();
        CreateMap<UpdateLeaveAllocationCommand, LeaveTypeVM>().ReverseMap();
    }
}
