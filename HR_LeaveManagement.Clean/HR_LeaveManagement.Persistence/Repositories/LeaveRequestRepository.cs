using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Domain;
using HR_LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(HrDbContext context) : base(context){
    }
    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
        return await _dbContext.Set<LeaveRequest>()
            .Include(q => q.LeaveType)
            .ToListAsync();
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
    {
        var leaveRequests = await _dbContext.leaveRequests
                .Where(q => q.RequestingEmployeeId == userId)
                .Include(q => q.LeaveType)
                .ToListAsync();
        return leaveRequests;
    }

    public async Task<LeaveRequest?> GetLeaveRequestWithDetails(int id)
    {
        var leaveRequest = await _dbContext.leaveRequests
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);

        return leaveRequest;
    }
}
