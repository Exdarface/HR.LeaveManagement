using FluentValidation;
using HR.LeaveManagement.Application.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.DTOs.LeaveTypes.Validators
{
    public class UpdateLeaveTypeDTOValidator : AbstractValidator<UpdateLeaveTypeDTO>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public UpdateLeaveTypeDTOValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            Include(new ILeaveTypeDTOValidator());

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MustAsync(async (id, token) => await _leaveTypeRepository.Exists(id)
                ).WithMessage("{PropertyName} does not exist");
        }
    }
}
