using TagManagement.Data;
using TagManagement.Dtos;
using TagManagement.Enums;
using TagManagement.Models;

namespace TagManagement.Services
{
    public class TagService : IItagService
    {
        private readonly CaseDBContext _caseContext;

        public TagService(CaseDBContext caseContext)
        {
            _caseContext = caseContext;
        }

        public async Task<Tags> CreateTagAsync(TagCreateDto dto)
        {
            int? weighEventId = dto.WeightEventId;

            // Only auto tags should attach a weigh event
            if (dto.WeightEventId == null && dto.UserId == null)
            {
                // AUTO TAG with no provided weighEventId → create weigh event
                var weighEvent = new WeightEvents
                {
                    VehicleReg = dto.VehicleReg,
                    Timestamp = DateTime.UtcNow,
                    ImageUrl = dto.ImageUrl ?? ""
                };

                _caseContext.WeightEvents.Add(weighEvent);
                await _caseContext.SaveChangesAsync();

                weighEventId = weighEvent.Id;
            }

            var tag = new Tags
            {
                UserId = dto.UserId,
                VehicleReg = dto.VehicleReg,
                Reason = dto.Reason,
                ImageUrl = dto.ImageUrl,
                StationId = dto.StationId,
                EventTimestamp = dto.EventTimeStamp,
                Notes = dto.Notes,
                WeighEventId = weighEventId,

                TagType = dto.UserId != 0 ? "MANUAL" : "AUTO"
            };

            _caseContext.Tags.Add(tag);
            await _caseContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tags> CloseTagAsync(int tagId, CloseTagDto dto)
        {
            var tag = await _caseContext.Tags.FindAsync(tagId);

            if (tag == null)
                throw new Exception("Tag not found.");

            if (tag.Status == TagStatus.closed)
                throw new Exception("Tag is already closed.");

            if (dto.ClosedByStationId != tag.StationId)
                throw new Exception("This station is not allowed to close this tag.");

            tag.ClosedByUserId = dto.ClosedByUserId;
            tag.ClosedAt = DateTime.UtcNow;
            tag.Status = TagStatus.closed;
            tag.Notes = dto.Notes;

            await _caseContext.SaveChangesAsync();
            return tag;
        }
    }
}
