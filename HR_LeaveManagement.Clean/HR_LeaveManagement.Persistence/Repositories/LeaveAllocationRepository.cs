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

    public async Task<List<LeaveAllocation>> GetAllocationsWithDetails()
    {
        return await _dbContext.Set<LeaveAllocation>()
            .Include(q => q.LeaveType)
            .ToListAsync();
    }
}
