using AutoMapper;
using HR_LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR_LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.MappingProfiles
{
    public class LeaveTypeProfile : Profile
    {
        public LeaveTypeProfile()
        {
            CreateMap<LeaveTypeDTO, LeaveType>().ReverseMap();
        }
    }
}
