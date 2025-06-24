using Mng_shifts.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mng_shifts.Core.DTOs
{

public class SwapProposalDTO
    {
        public int Id { get; set; }
        public int SwapRequestId { get; set; }
        public int ProposedShiftId { get; set; }
        public DateTime ProposedAt { get; set; }
        public SwapProposalStatus Status { get; set; }
    }

}
