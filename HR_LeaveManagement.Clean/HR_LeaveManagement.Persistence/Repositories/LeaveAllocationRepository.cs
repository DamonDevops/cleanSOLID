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

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HrDbContext context) : base(context)
    {

    }

    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await _dbContext.AddRangeAsync(allocations);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
    {
        return await _dbContext.leaveAllocations.AnyAsync(q => q.EmployeeId == userId
        && q.LeaveTypeId == leaveTypeId
        && q.Period == period
        );
    }
    public async Task<List<LeaveAllocation>> GetAllocationsWithDetails()
    {
        return await _dbContext.Set<LeaveAllocation>()
            .Include(q => q.LeaveType)
            .ToListAsync();
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
    {
        var leaveAllocations = await _dbContext.leaveAllocations.Where(q => q.EmployeeId == userId)
               .Include(q => q.LeaveType)
               .ToListAsync();
        return leaveAllocations;
    }

    public async Task<LeaveAllocation?> GetLeaveAllocationWithDetails(int id)
    {
        var leaveAllocation = await _dbContext.leaveAllocations
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);

        return leaveAllocation;
    }

    public async Task<LeaveAllocation?> GetUserAllocations(string userId, int leaveTypeId)
    {
        return await _dbContext.leaveAllocations.FirstOrDefaultAsync(q => q.EmployeeId == userId
                                        && q.LeaveTypeId == leaveTypeId);
    }
}
