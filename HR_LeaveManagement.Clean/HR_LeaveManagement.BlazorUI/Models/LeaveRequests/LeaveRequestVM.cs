using HR_LeaveManagement.BlazorUI.Models.LeaveTypes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HR_LeaveManagement.BlazorUI.Models.LeaveRequests;

public class LeaveRequestVM
{
    public int Id { get; set; }
    [Display(Name = "Date Requested")]
    public DateTime DateRequested { get; set; }
    [Display(Name = "Date Actioned")]
    public DateTime DateActioned { get; set; }
    [Display(Name = "Approval State")]
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }

    public LeaveTypeVM LeaveType{ get; set; }
    public EmployeeVM Employee { get; set; }

    [Display(Name = "Starting Date")]
    [Required]
    public DateTime? StartingDate { get; set; }
    [Display(Name = "Ending Date")]
    [Required]
    public DateTime? EndingDate { get; set; }

    [Display(Name = "Leave Type")]
    [Required]
    public int LeaveTypeId { get; set; }

    [Display(Name = "Comments")]
    [MaxLength(300)]
    public string? RequestComments { get; set; }

}
