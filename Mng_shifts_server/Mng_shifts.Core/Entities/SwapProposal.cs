using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Mng_shifts.Core.Entities
{
    [Table("SwapProposal")]
    public class SwapProposal
    {
            [Key]
            public int Id { get; set; }

            [Required]
            public int SwapRequestId { get; set; }

            [ForeignKey(nameof(SwapRequestId))]
            public SwapRequest SwapRequest { get; set; }

            [Required]
            public int ProposedShiftId { get; set; }

            [ForeignKey(nameof(ProposedShiftId))]
            public Shift ProposedShift { get; set; }

            [Required]
            public DateTime ProposedAt { get; set; }

            [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]

        public SwapProposalStatus Status { get; set; }
        }



    
    public enum SwapProposalStatus
    {
        WaitingForApproval,
        Approved,
        Rejected
    }

}
