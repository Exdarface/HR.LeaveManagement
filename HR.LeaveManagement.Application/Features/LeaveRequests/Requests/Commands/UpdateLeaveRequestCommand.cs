using HR.LeaveManagement.Application.DTOs.LeaveRequests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands
{
    public class UpdateLeaveRequestCommand : IRequest
    {
        public UpdateLeaveRequestDTO LeaveRequest { get; set; }

        public ChangeLeaveRequestApprovalDTO? ChangeLeaveRequestApproval { get; set; }
    }
}
