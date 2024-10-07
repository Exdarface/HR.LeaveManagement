using AutoMapper;
using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.DTOs.LeaveRequests.Validators;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Models;
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
    public class CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository,IEmailSender emailSender, IMapper mapper) : IRequestHandler<CreateLeaveRequestCommand, Guid>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository = leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IMapper _mapper = mapper;

        public async Task<Guid> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestDTOValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequest, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToString());
            }
            var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request.LeaveRequest);
            await _leaveRequestRepository.Add(leaveRequest);

            var email = new Email()
            {
                To = "leave-request@org.com",
                Subject = $"Your leave request for {request.LeaveRequest.StartDate:D} to {request.LeaveRequest.EndDate:D} has been submitted successfully.",
                Body = "Leave Request Submitted"
            };

            try
            {
                await _emailSender.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                // log or manage the exception
            }
            return leaveRequest.Id;
        }
    }
}
