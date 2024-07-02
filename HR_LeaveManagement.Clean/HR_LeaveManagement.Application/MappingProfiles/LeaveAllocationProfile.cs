using AutoMapper;
using HR_LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR_LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR_LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using HR_LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR_LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.MappingProfiles;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        CreateMap<LeaveAllocationDTO, LeaveAllocation>().ReverseMap();
        CreateMap<LeaveAllocation, LeaveAllocationDetailDTO>();
        CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
        CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>();
    }
}
