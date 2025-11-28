using TagManagement.Dtos;
using TagManagement.Models;

namespace TagManagement.Services
{
    public interface IItagService
    {
        Task<Tags> CreateTagAsync(TagCreateDto dto);
        Task<Tags> CloseTagAsync(int tagId, CloseTagDto closedto);
        Task<IEnumerable<Tags>> GetAllTagsAsync();
        Task<IEnumerable<Tags>> SearchTagsAsync(TagSearchDto searchDto);
    }
}
