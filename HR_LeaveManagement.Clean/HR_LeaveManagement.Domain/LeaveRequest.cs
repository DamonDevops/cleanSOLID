using HR_LeaveManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Domain;

public class LeaveRequest : BaseEntity
{
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public DateTime RequestedDate { get; set; }
    public string? RequestComments { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }

    public string RequestingEmployeeId { get; set; } = string.Empty;
}
