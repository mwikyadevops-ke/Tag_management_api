using System.ComponentModel.DataAnnotations;

namespace TagManagement.Dtos
{
    public class TagCreateDto
    {
        public int? UserId { get; set; }   // Who created the tag (null for automatic tags, set for manual tags)
        
        [Required(ErrorMessage = "VehicleReg is required")]
        public string VehicleReg { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Reason is required")]
        public string Reason { get; set; } = string.Empty;
        
        public string? ImageUrl { get; set; }
        
        [Required(ErrorMessage = "StationId is required")]
        public int StationId { get; set; }
        
        public DateTime EventTimeStamp { get; set; } = DateTime.UtcNow;
        
        public string? Notes { get; set; }
    }
}
