using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class NotFoundException(string message, object key) : ApplicationException($"Entity \"{message}\" ({key}) was not found.")
    {
    }
}
