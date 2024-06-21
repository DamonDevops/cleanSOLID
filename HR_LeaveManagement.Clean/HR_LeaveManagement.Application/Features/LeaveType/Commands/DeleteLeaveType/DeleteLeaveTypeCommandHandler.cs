using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR_LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        ILeaveTypeRepository _leaveTypeRepository;
        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            //Get from DB 
            var toDelete = await _leaveTypeRepository.GetByIdAsync(request.Id);

            //Verify DTO exist
            if(toDelete == null)
            {
                throw new NotFoundException(nameof(LeaveType), request.Id);
            }

            //remove from DB
            await _leaveTypeRepository.DeleteAsync(toDelete);

            //Return record ID
            return Unit.Value;
        }
    }
}
