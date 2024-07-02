using Microsoft.AspNetCore.Mvc;

namespace HR_LeaveManagement.API.Models;

public class CustomValidationProblemDetails : ProblemDetails
{
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}
