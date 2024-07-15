using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;

public record GetLeaveAllocationsQuery : IRequest<List<LeaveAllocationDTO>>
{
    public bool IsLoggedInUser { get; set; }
};
