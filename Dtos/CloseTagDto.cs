namespace TagManagement.Dtos
{
    public class CloseTagDto
    {

        public required string Notes { get; set; }
        public required int ClosedByUserId { get; set; }
        public int ClosedByStationId { get; set; }

    }
}
