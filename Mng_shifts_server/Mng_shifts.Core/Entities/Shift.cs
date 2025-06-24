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
    [Table("Shift")]
    public class Shift
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ShiftType Type { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]

        public ShiftStatus Status { get; set; }

        public SwapRequest SwapRequest { get; set; }

        //public ICollection<SwapProposal> SwapProposals { get; set; } = new List<SwapProposal>();

    }
    public enum ShiftType
    {
        Morning,
        Evening,
        Night
    }

    public enum ShiftStatus
    {
        Regular,             // לא קרה כלום
        SwapRequested,       // המשתמש שלח בקשת החלפה
        OfferedForSwap,      // מישהו אחר הציע החלפה עליה
        Swapped              // בוצעה החלפה
    }
}
