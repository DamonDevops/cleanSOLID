using HR_LeaveManagement.Domain;
using HR_LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR_LeaveManagement.Persistence.IntegrationTests;

public class HrDbContextTest
{
    private HrDbContext _hrDbContext;

    public HrDbContextTest()
    {
        var dbOptions = new DbContextOptionsBuilder<HrDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _hrDbContext = new HrDbContext(dbOptions);
    }

    [Fact]
    public async void Save_SetDateCreatedValue()
    {
        var leavetype = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "test vacantion"
        };

        await _hrDbContext.leaveTypes.AddAsync(leavetype);
        await _hrDbContext.SaveChangesAsync();

        leavetype.DateCreated.ShouldNotBeNull();

    }
    [Fact]
    public async void Save_SetDateModifiedValue()
    {
        var leavetype = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "test vacantion"
        };

        await _hrDbContext.leaveTypes.AddAsync(leavetype);
        await _hrDbContext.SaveChangesAsync();

        leavetype.DateModified.ShouldNotBeNull();
    }
}