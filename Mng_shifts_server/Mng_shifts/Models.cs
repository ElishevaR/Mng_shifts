using Mng_shifts.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Mng_shifts.Api
{
 
        public class CreateEmployeeModel
        {
            [Required]
            public string FullName { get; set; }

            [Required, EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public class CreateShiftModel
        {
            [Required]
            public int EmployeeId { get; set; }

            [Required]
            public DateTime Date { get; set; }

            [Required]
            public ShiftType Type { get; set; }
        }

        public class UpdateShiftStatusModel
        {
            [Required]
            public ShiftStatus Status { get; set; }
        }

        public class CreateSwapRequestModel
        {
            [Required]
            public int ShiftId { get; set; }

            public string Comment { get; set; }
        }

        public class CreateSwapProposalModel
        {
            [Required]
            public int SwapRequestId { get; set; }

            [Required]
            public int ProposedShiftId { get; set; }
        }

        public class UpdateProposalStatusModel
        {
            [Required]
            public SwapProposalStatus Status { get; set; }
        }
}
