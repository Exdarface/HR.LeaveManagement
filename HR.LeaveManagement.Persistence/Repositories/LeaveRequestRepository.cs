using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository(LeaveManagementDbContext dbContext) : GenericRepository<LeaveRequest>(dbContext), ILeaveRequestRepository
    {
    }
}
