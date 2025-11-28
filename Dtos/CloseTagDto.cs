using System.ComponentModel.DataAnnotations;

namespace TagManagement.Dtos
{
    public class CloseTagDto
    {
        [Required]
        public required string CloseReason { get; set; } // Reason for closing the tag
        
        [Required]
        public required int ClosedByUserId { get; set; }
        
        [Required]
        public int ClosedByStationId { get; set; }
        
        public string? Notes { get; set; }
    }
}
