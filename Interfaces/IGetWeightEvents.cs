
using TagManagement.Dtos;
using TagManagement.Models;
namespace TagManagement.Services
{
    public interface IIGetWeightEvents
    {
        Task<WeightEvents> CreateWeighEventAsync(WeightEventDto dto);
    }
}
