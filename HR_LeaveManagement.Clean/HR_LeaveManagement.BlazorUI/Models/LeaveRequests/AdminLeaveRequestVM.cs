namespace HR_LeaveManagement.BlazorUI.Models.LeaveRequests
{
    public class AdminLeaveRequestVM
    {
        public int TotalRequests { get; set; }
        public int ApprovedRequests { get; set; }
        public int PendingRequests { get; set; }
        public int RejectedRequests { get; set; }
        public List<LeaveRequestVM> LeaveRequestVMs { get; set; } = new List<LeaveRequestVM>();
    }
}
