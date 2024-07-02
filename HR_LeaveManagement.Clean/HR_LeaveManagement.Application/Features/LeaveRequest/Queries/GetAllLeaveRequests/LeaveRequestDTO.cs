﻿using HR_LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public class LeaveRequestDTO
{
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public LeaveTypeDTO? LeaveType { get; set; }
    public DateTime RequestedDate { get; set; }
    public bool? Approved { get; set; }

    public string RequestingEmployeeId { get; set; } = string.Empty;
}