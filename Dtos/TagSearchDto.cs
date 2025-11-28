namespace TagManagement.Dtos
{
    public class TagSearchDto
    {
        public string? VehicleReg { get; set; }
        public string? TagType { get; set; } // AUTO or MANUAL
        public TagManagement.Enums.TagStatus? Status { get; set; }
        public int? StationId { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public string? Reason { get; set; }
    }
}

