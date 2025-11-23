using TagManagement.Data;
using TagManagement.Dtos;
using TagManagement.Models;

namespace TagManagement.Services
{
    public class WeighEventService : IIGetWeightEvents
    {

        private readonly CaseDBContext _db;
        private readonly IItagService _tagService;
        public WeighEventService(CaseDBContext db) { 
            _db = db;
        }
        public async Task<WeightEvents> CreateWeighEventAsync(WeightEventDto dto)
        {
            var weightEvent = new WeightEvents {
                VehicleReg = dto.VehicleReg,
                GrossWeight = dto.GrossWeight,
                AxleWeights = dto.AxleWeights,
                Timestamp = DateTime.Parse(dto.Timestamp),
                Location = dto.Location,
                ImageUrl = dto.ImageUrl,
                Source = "HSWIM"
            };
            _db.WeightEvents.Add(weightEvent);
            await _db.SaveChangesAsync();
         return weightEvent;
        }
    }
}
