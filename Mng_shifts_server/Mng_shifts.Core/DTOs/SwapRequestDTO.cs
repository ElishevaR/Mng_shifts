using Mng_shifts.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mng_shifts.Core.DTOs
{
    public class SwapRequestDTO
    {
        public int Id { get; set; }
        public int ShiftId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public SwapRequestStatus Status { get; set; }
    }

}
