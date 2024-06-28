using HR_LeaveManagement.Application.Models.EmailModels;

namespace HR_LeaveManagement.Application.Contracts.Email
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(EmailMessage email);
    }
}
