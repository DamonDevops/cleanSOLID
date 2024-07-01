using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR_LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public CreateLeaveRequestCommandHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
    {
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
    }

    public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(_leaveRequestRepository);
        var validatorResults = validator.Validate(request);

        if (validatorResults.Errors.Any())
        {
            throw new BadRequestException("Invalid LeaveRequest", validatorResults);
        }

        var toCreate = _mapper.Map<Domain.LeaveRequest>(request);

        await _leaveRequestRepository.CreateAsync(toCreate);

        return toCreate.Id;
    }
}
