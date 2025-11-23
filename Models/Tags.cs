

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TagManagement.Enums;
namespace TagManagement.Models
   
{
    public class Tags
    {

        [Key]
        public int Id { get; set; }
        [ForeignKey("WeighEvent")]
        public int? WeighEventId { get; set; }
        public WeightEvents? WeighEvent { get; set; }
        public string TagType { get; set; } = "AUTO"; // AUTO or MANUAL
        public string Reason { get; set; } = string.Empty;
        public int? UserId { get; set; } // null → auto; not null → manual
        public int? ClosedByUserId { get; set; }
        public string VehicleReg { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int StationId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime EventTimestamp { get; set; } = DateTime.UtcNow;
        public DateTime? ClosedAt { get; set; }
        public TagStatus Status { get; set; } = TagStatus.open;
        public string? Notes { get; set; }

    }
}
