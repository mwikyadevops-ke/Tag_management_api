namespace TagManagement.Dtos
{
    public class WeighEventResponseDto
    {
        public int Id { get; set; }
        public string VehicleReg { get; set; } = string.Empty;
        public decimal GrossWeight { get; set; }
        public decimal[] AxleWeights { get; set; } = [];
        public decimal OverloadAmount { get; set; }
        public DateTime Timestamp { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
