namespace TagManagement.Dtos
{
    public class WeightEventDto
    {
        public string VehicleReg { get; set; } = string.Empty;
        public decimal GrossWeight { get; set; }
        public decimal[] AxleWeights { get; set; } = [];
        public decimal OverloadAmount { get; set; }
        public string Timestamp { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;


    }
}
