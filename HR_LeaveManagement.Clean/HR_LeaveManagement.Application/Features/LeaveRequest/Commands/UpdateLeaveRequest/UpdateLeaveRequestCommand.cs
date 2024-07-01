using HR_LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public LeaveTypeDTO? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public DateTime RequestedDate { get; set; }
    public string? RequestComments { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }

    public string RequestingEmployeeId { get; set; } = string.Empty;
}
