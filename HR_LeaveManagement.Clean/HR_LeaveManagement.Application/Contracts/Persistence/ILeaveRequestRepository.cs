using HR_LeaveManagement.Domain;

namespace HR_LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetails();
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId);
    Task<LeaveRequest?> GetLeaveRequestWithDetails(int id);
}
