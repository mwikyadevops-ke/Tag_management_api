using System.ComponentModel.DataAnnotations;

namespace TagManagement.Dtos
{
    public class TagCreateFormDto
    {
        public int? UserId { get; set; }   // Who created the tag (null for automatic tags, set for manual tags)
        [Required]
        public string VehicleReg { get; set; } = string.Empty;
        [Required]
        public string Reason { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }  // Image file upload
        public string? ImageUrl { get; set; }  // Optional: existing image URL (alternative to file upload)
        [Required]
        public int StationId { get; set; }
        public DateTime? EventTimeStamp { get; set; }  // Optional: defaults to UtcNow if not provided
        public string? Notes { get; set; }
    }
}
