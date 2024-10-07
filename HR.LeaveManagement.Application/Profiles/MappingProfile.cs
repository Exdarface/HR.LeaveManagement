using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocations;
using HR.LeaveManagement.Application.DTOs.LeaveRequests;
using HR.LeaveManagement.Application.DTOs.LeaveTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.LeaveType, LeaveTypeDTO>().ReverseMap();
            CreateMap<Domain.LeaveAllocation, LeaveAllocationDTO>().ReverseMap();
            CreateMap<Domain.LeaveRequest, LeaveRequestDTO>().ReverseMap();
        }
    }
}
