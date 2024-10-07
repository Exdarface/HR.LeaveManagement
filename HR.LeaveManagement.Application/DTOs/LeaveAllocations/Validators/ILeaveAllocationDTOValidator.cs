using FluentValidation;
using HR.LeaveManagement.Application.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllocations.Validators
{
    public class ILeaveAllocationDTOValidator : AbstractValidator<ILeaveAllocationDTO>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public ILeaveAllocationDTOValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(p => p.NumberOfDays).NotEmpty().GreaterThan(0);
            RuleFor(p => p.Period).NotEmpty().GreaterThan(DateTime.Now.Year);
            RuleFor(p => p.LeaveTypeId).NotEmpty().MustAsync(async (id, token) => await _leaveTypeRepository.Exists(id));
            
        }
    }
}
