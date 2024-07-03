using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.UnitTests.Mocks;

public class MocksLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetMockLeaveTypesRepository()
    {
        var leavetypes = new List<LeaveType>
        {
            new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name = "test vacantion"
            },
            new LeaveType
            {
                Id = 2,
                DefaultDays = 5,
                Name = "test sickness"
            },
            new LeaveType
            {
                Id = 3,
                DefaultDays = 15,
                Name = "test maternity"
            },
        };

        var mockRepo = new Mock<ILeaveTypeRepository>();
        mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leavetypes);
        mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
            .Returns((LeaveType leavetype) => {
                leavetypes.Add(leavetype);
                return Task.FromResult(leavetype);
            });
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => {
            return leavetypes.FirstOrDefault(lt => lt.Id == id);
            });

        return mockRepo;
    }
}
