using AutoMapper;
using HR_LeaveManagement.BlazorUI.Models;
using HR_LeaveManagement.BlazorUI.Models.LeaveRequests;
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

        CreateMap<LeaveRequestDTO, LeaveRequestVM>()
            .ForMember(q => q.RequestedDate, opt => opt.MapFrom(x => x.RequestedDate.DateTime))
            .ForMember(q => q.StartingDate, opt => opt.MapFrom(x => x.StartingDate.DateTime))
            .ForMember(q => q.EndingDate, opt => opt.MapFrom(x => x.EndingDate.DateTime))
            .ReverseMap();
        CreateMap<LeaveRequestDetailDTO, LeaveRequestVM>()
            .ForMember(q => q.RequestedDate, opt => opt.MapFrom(x => x.RequestedDate.DateTime))
            .ForMember(q => q.StartingDate, opt => opt.MapFrom(x => x.StartingDate.DateTime))
            .ForMember(q => q.EndingDate, opt => opt.MapFrom(x => x.EndingDate.DateTime))
            .ReverseMap();
        CreateMap<CreateLeaveRequestCommand, LeaveRequestVM>().ReverseMap();
        CreateMap<UpdateLeaveRequestCommand, LeaveRequestVM>().ReverseMap();

        CreateMap<LeaveAllocationDTO, LeaveTypeVM>().ReverseMap();
        CreateMap<LeaveAllocationDetailDTO, LeaveTypeVM>().ReverseMap();
        CreateMap<CreateLeaveAllocationCommand, LeaveTypeVM>().ReverseMap();
        CreateMap<UpdateLeaveAllocationCommand, LeaveTypeVM>().ReverseMap();

        CreateMap<EmployeeVM, Employee>().ReverseMap();
    }
}
