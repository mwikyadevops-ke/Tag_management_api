

using TagManagement.Dtos;
using TagManagement.Models;
    namespace TagManagement.Services
{
    public interface IItagService
    {
        //Task<Tags> CreateAutoTagAsync(WeightEvents weighEvent);
        Task<Tags> CreateTagAsync(TagCreateDto dto);
        Task<Tags> CloseTagAsync(int tagId, CloseTagDto closedto);
    }
}
