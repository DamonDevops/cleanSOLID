using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Commands.Shared;

public abstract class BaseLeaveRequest
{
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public int LeaveTypeId { get; set; }
}
