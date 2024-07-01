using HR_LeaveManagement.Domain;

namespace HR_LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<List<LeaveAllocation>> GetAllocationsWithDetails();
}
