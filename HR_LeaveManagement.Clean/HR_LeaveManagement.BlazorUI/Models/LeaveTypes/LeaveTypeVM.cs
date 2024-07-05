using System.ComponentModel.DataAnnotations;

namespace HR_LeaveManagement.BlazorUI.Models.LeaveTypes;

public class LeaveTypeVM
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Display(Name= "Minimum number of days")]
    public int DefaultDays { get; set; }
}
