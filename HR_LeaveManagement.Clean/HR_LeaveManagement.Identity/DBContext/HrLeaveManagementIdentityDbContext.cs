using HR_LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HR_LeaveManagement.Identity.DBContext;

public class HrLeaveManagementIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public HrLeaveManagementIdentityDbContext(DbContextOptions<HrLeaveManagementIdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(HrLeaveManagementIdentityDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}
