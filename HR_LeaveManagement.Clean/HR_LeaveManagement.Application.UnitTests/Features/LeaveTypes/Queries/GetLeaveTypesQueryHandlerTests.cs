using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR_LeaveManagement.Application.MappingProfiles;
using HR_LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR_LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries;

public class GetLeaveTypesQueryHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private IMapper _mapper;
    private Mock<IAppLogger<GetLeaveTypesQueryHandler>> _mockLogger;

    public GetLeaveTypesQueryHandlerTests()
    {
        _mockRepo = MocksLeaveTypeRepository.GetMockLeaveTypesRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        }
        );

        _mapper = mapperConfig.CreateMapper();
        _mockLogger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
    }

    [Fact]
    public async Task GetLeaveTypesTest()
    {
        var handler = new GetLeaveTypesQueryHandler(_mapper, _mockRepo.Object, _mockLogger.Object);

        var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);
        result.ShouldBeOfType<List<LeaveTypeDTO>>();
        result.Count.ShouldBe(3);
    }
}
