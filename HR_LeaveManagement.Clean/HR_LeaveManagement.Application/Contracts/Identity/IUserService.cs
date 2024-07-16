using HR_LeaveManagement.Application.Models.IdentityModels;

namespace HR_LeaveManagement.Application.Contracts.Identity;

public interface IUserService
{
    Task<List<Employee>> GetEmployees();
    Task<Employee> GetEmployeeById(string userId);

    public string UserId { get; }
}
