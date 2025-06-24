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
    [Table("SwapRequest")]
    public class SwapRequest
    {
            [Key]
            public int Id { get; set; }

            [Required]
            public int ShiftId { get; set; }

            [ForeignKey(nameof(ShiftId))]
            public Shift Shift { get; set; }

            public string Comment { get; set; }

            [Required]
            public DateTime CreatedAt { get; set; }

            [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]

        public SwapRequestStatus Status { get; set; }


        public ICollection<SwapProposal> SwapProposals { get; set; } = new List<SwapProposal>();

    }
    public enum SwapRequestStatus
    {
        Open,
        Completed,
        Cancelled
    }

}
