namespace HR_LeaveManagement.BlazorUI.Contracts;

public interface IAuthService
{
    Task<bool> AuthenticateAsync(string email, string password);
    Task<bool> RegisterAsync(string firstname, string lastname, string username, string email, string password);
    Task Logout();
}
