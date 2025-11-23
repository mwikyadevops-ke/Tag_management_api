using System.ComponentModel.DataAnnotations;

namespace TagManagement.Models
{
    public class WeightEvents
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public string VehicleReg { get; set; } = string.Empty;
        public decimal GrossWeight { get; set; }
        public decimal[] AxleWeights { get; set; } = [];
        public DateTime Timestamp { get; set; }
        public string Location { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Source { get; set; } = "HSWIM";
        public bool Processed { get; set; } = false;
        public ICollection<Tags> Tags { get; set; }
    }
}
