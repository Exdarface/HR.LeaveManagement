using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequests;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries
{
    public class GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper) : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestDTO>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository = leaveRequestRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<LeaveRequestDTO>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
        {
            var leaveTypes = await _leaveRequestRepository.GetAll();
            return _mapper.Map<List<LeaveRequestDTO>>(leaveTypes);
        }
    }
}
