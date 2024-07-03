using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR_LeaveManagement.Application.MappingProfiles;
using HR_LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR_LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries;

public class GetLeaveTypeDetailsQueryHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<ILeaveTypeRepository> _mockRepo;

    public GetLeaveTypeDetailsQueryHandlerTest()
    {
        _mockRepo = MocksLeaveTypeRepository.GetMockLeaveTypesRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        }
        );

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task GetLeaveTypeDetailsTest()
    {
        var handler = new GetLeaveTypeDetailsQueryHandler(_mapper, _mockRepo.Object);

        var result = await handler.Handle(new GetLeaveTypeDetailsQuery { Id = 2 }, CancellationToken.None);
        result.ShouldBeOfType<LeaveTypeDetailDTO>();
    }
}
