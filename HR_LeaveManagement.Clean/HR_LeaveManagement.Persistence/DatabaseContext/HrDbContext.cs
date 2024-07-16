using HR_LeaveManagement.Application.Contracts.Identity;
using HR_LeaveManagement.Domain;
using HR_LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HR_LeaveManagement.Persistence.DatabaseContext;

public class HrDbContext : DbContext
{
    private readonly IUserService _userService;

    public HrDbContext(DbContextOptions<HrDbContext> options, IUserService userService) : base(options)
    {
        _userService = userService;
    }

    public DbSet<LeaveType> leaveTypes { get; set; }
    public DbSet<LeaveAllocation> leaveAllocations { get; set; }
    public DbSet<LeaveRequest> leaveRequests { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach(var entry in base.ChangeTracker.Entries<BaseEntity>().Where(q => q.State == EntityState.Modified || q.State == EntityState.Added))
        {
            entry.Entity.DateModified = DateTime.Now;
            entry.Entity.ModifiedBy = _userService.UserId;
            if(entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.Now;
                entry.Entity.CreatedBy = _userService.UserId;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrDbContext).Assembly);

        //modelBuilder.Entity<LeaveType>().HasData(
        //    new LeaveType { Id = 1, Name = "Vacations", DefaultDays = 10, DateCreated = DateTime.Now, DateModified = DateTime.Now }
        //);

        base.OnModelCreating(modelBuilder);
    }
}
