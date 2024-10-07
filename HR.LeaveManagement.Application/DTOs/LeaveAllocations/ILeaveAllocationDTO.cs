using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllocations
{
    public interface ILeaveAllocationDTO
    {
        public int NumberOfDays { get; set; }
        public int Period { get; set; }
        public Guid LeaveTypeId { get; set; }
    }
}
