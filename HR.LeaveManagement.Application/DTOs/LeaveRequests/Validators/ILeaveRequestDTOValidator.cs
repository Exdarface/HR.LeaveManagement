using FluentValidation;
using HR.LeaveManagement.Application.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequests.Validators
{
    public class ILeaveRequestDTOValidator : AbstractValidator<ILeaveRequestDTO>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public ILeaveRequestDTOValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(p => p.StartDate).NotEmpty()
                .GreaterThan(DateTime.Now)
                .LessThan(p => p.EndDate)
                .WithMessage("{PropertyName} must be greater than {ComparisonValue} and less than End Date");
            RuleFor(p => p.EndDate).NotEmpty()
                .GreaterThan(p => p.StartDate).WithMessage("{PropertyName} must be greater than Start Date");
            RuleFor(p => p.LeaveTypeId).NotEmpty()
                .MustAsync(async (id, token) => await _leaveTypeRepository.Exists(id))
                .WithMessage("{PropertyName} does not exist");
        }
    }
}
