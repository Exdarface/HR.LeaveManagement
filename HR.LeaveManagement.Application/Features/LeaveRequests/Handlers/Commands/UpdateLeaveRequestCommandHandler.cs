using AutoMapper;
using FluentValidation;
using HR.LeaveManagement.Application.DTOs.LeaveRequests.Validators;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,ILeaveTypeRepository leaveTypeRepository,  IMapper mapper) : IRequestHandler<UpdateLeaveRequestCommand>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository = leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository;
        private readonly IMapper _mapper = mapper;
        public async Task Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveRequestDTOValidator(_leaveRequestRepository, _leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequest, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToString());
            }
            var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request.LeaveRequest);
            if (request.ChangeLeaveRequestApproval != null)
            {
                leaveRequest.Approved = request.ChangeLeaveRequestApproval.Approved;
                leaveRequest.RequestedDate = DateTime.Now;
            }
            await _leaveRequestRepository.Update(leaveRequest);
        }
    }
}
