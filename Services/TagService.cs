using Microsoft.EntityFrameworkCore;
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
            // Determine tag type based on UserId
            bool isManual = dto.UserId.HasValue && dto.UserId.Value > 0;
            string tagType = isManual ? "MANUAL" : "AUTO";

            var tag = new Tags
            {
                UserId = dto.UserId,
                VehicleReg = dto.VehicleReg,
                Reason = dto.Reason,
                ImageUrl = dto.ImageUrl,
                StationId = dto.StationId,
                EventTimestamp = dto.EventTimeStamp,
                Notes = dto.Notes,
                TagType = tagType,
                CreatedAt = DateTime.UtcNow,
                Status = TagStatus.open
            };

            _caseContext.Tags.Add(tag);
            await _caseContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tags> CloseTagAsync(int tagId, CloseTagDto dto)
        {
            var tag = await _caseContext.Tags.FindAsync(tagId);

            if (tag == null)
                throw new ArgumentException("Tag not found.");

            if (tag.Status == TagStatus.closed)
                throw new InvalidOperationException("Tag is already closed.");

            // Validate that the tag can only be closed at the station where it was created
            if (dto.ClosedByStationId != tag.StationId)
                throw new UnauthorizedAccessException($"This tag can only be closed at Station {tag.StationId}. Attempted to close at Station {dto.ClosedByStationId}.");

            tag.ClosedByUserId = dto.ClosedByUserId;
            tag.ClosedAt = DateTime.UtcNow;
            tag.Status = TagStatus.closed;
            tag.CloseReason = dto.CloseReason;
            
            // Update notes if provided, otherwise keep existing notes
            if (!string.IsNullOrWhiteSpace(dto.Notes))
            {
                tag.Notes = string.IsNullOrWhiteSpace(tag.Notes) 
                    ? dto.Notes 
                    : $"{tag.Notes}\nClosed: {dto.Notes}";
            }

            await _caseContext.SaveChangesAsync();
            return tag;
        }

        public async Task<IEnumerable<Tags>> GetAllTagsAsync()
        {
            return await _caseContext.Tags
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tags>> SearchTagsAsync(TagSearchDto searchDto)
        {
            var query = _caseContext.Tags
                .AsQueryable();

            // Apply filters based on search criteria
            if (!string.IsNullOrWhiteSpace(searchDto.VehicleReg))
            {
                query = query.Where(t => t.VehicleReg.Contains(searchDto.VehicleReg));
            }

            if (!string.IsNullOrWhiteSpace(searchDto.TagType))
            {
                query = query.Where(t => t.TagType == searchDto.TagType);
            }

            if (searchDto.Status.HasValue)
            {
                query = query.Where(t => t.Status == searchDto.Status.Value);
            }

            if (searchDto.StationId.HasValue)
            {
                query = query.Where(t => t.StationId == searchDto.StationId.Value);
            }

            if (searchDto.UserId.HasValue)
            {
                query = query.Where(t => t.UserId == searchDto.UserId.Value);
            }

            if (searchDto.CreatedFrom.HasValue)
            {
                query = query.Where(t => t.CreatedAt >= searchDto.CreatedFrom.Value);
            }

            if (searchDto.CreatedTo.HasValue)
            {
                query = query.Where(t => t.CreatedAt <= searchDto.CreatedTo.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchDto.Reason))
            {
                query = query.Where(t => t.Reason.Contains(searchDto.Reason));
            }

            return await query
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }
    }
}
