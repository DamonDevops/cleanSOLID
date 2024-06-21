using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR_LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    IMapper _mapper;
    ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate data
        var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
        var validationResults = await validator.ValidateAsync(request);

        if (validationResults.Errors.Any())
        {
            throw new BadRequestException("Invalid LeaveType", validationResults);
        }

        //Convert to DTO
        var toCreate = _mapper.Map<Domain.LeaveType>(request);

        //Add to DB
        await _leaveTypeRepository.CreateAsync(toCreate);

        //Return record ID
        return toCreate.Id;
    }
}
