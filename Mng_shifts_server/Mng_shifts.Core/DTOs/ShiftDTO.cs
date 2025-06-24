using Mng_shifts.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mng_shifts.Core.DTOs
{
    public class ShiftDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ShiftType Type { get; set; }
        public ShiftStatus Status { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }  // אופציונלי
    }

}
