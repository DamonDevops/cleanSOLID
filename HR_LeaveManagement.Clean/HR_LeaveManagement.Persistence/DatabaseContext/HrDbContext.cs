using HR_LeaveManagement.Domain;
using HR_LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HR_LeaveManagement.Persistence.DatabaseContext;

public class HrDbContext : DbContext
{
    public HrDbContext(DbContextOptions<HrDbContext> options) : base(options)
    { 
    }

    public DbSet<LeaveType> leaveTypes { get; set; }
    public DbSet<LeaveAllocation> leaveAllocations { get; set; }
    public DbSet<LeaveRequest> leaveRequests { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach(var entry in base.ChangeTracker.Entries<BaseEntity>().Where(q => q.State == EntityState.Modified || q.State == EntityState.Added))
        {
            entry.Entity.DateModified = DateTime.Now;
            if(entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.Now;
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
