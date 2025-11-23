using System.ComponentModel.DataAnnotations;
using TagManagement.Models;

namespace TagManagement.Dtos
{
    public class TagCreateDto
    {
        public  int UserId { get; set; }   // Who created the tag
        public string VehicleReg { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int StationId { get; set; }
        public DateTime EventTimeStamp { get; set; }= DateTime.UtcNow;
        public int? WeightEventId { get; set; }   //links to weigh events
        public int? WeighEventType { get; set; }
        public string? Notes { get; set; }
    }
}
