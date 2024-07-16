using AutoMapper;
using HR_LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR_LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR_LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;
using HR_LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using HR_LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.MappingProfiles;

public class LeaveRequestProfile : Profile
{
    public LeaveRequestProfile()
    {
        CreateMap<LeaveRequestDTO, LeaveRequest>().ReverseMap();
        CreateMap<LeaveRequestDetailDTO, LeaveRequest>().ReverseMap();
        CreateMap<LeaveRequest, LeaveRequestDetailDTO>();
        CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
        CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
    }
}
