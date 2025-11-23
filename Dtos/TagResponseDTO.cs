
namespace TagManagement.Dtos
{
    public class TagResponseDTO
    {
       public int Id { get; set; }
        public string TagType { get; set; } = "";
        public string Reason { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
