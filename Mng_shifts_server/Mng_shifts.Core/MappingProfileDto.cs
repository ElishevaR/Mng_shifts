using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mng_shifts.Core
{
    using AutoMapper;
    using Mng_shifts.Core.DTOs;
    using Mng_shifts.Core.Entities;
    using Mng_shifts.Core.DTOs;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    public class MappingProfileDTO : Profile
    {
        public MappingProfileDTO()
        {
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<Shift, ShiftDTO>();
            CreateMap<SwapProposal, SwapProposalDTO>();
            CreateMap<SwapRequest, SwapRequestDTO>();
        }
    }

}
