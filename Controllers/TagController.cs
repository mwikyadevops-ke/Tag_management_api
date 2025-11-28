using Microsoft.AspNetCore.Mvc;
using TagManagement.Dtos;
using TagManagement.Services;

namespace TagManagement.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagController : ControllerBase
    {

        private readonly IItagService _tagService;
        private readonly IFileUploadService _fileUploadService;
        
        public TagController(IItagService tagService, IFileUploadService fileUploadService)
        {
            _tagService = tagService;
            _fileUploadService = fileUploadService;
        }

        [HttpPost("create")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateTag([FromBody] TagCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await CreateTagInternal(dto);
        }

        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateTagWithFile([FromForm] TagCreateFormDto formDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            string? imageUrl = null;
            
            // Handle image upload if provided
            if (formDto.Image != null && formDto.Image.Length > 0)
            {
                try
                {
                    imageUrl = await _fileUploadService.SaveImageAsync(formDto.Image);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { error = $"Image upload failed: {ex.Message}" });
                }
            }
            
            // Convert form DTO to standard DTO
            var dto = new TagCreateDto
            {
                UserId = formDto.UserId,
                VehicleReg = formDto.VehicleReg ?? string.Empty,
                Reason = formDto.Reason ?? string.Empty,
                ImageUrl = imageUrl ?? formDto.ImageUrl,
                StationId = formDto.StationId,
                EventTimeStamp = formDto.EventTimeStamp ?? DateTime.UtcNow,
                Notes = formDto.Notes
            };

            return await CreateTagInternal(dto);
        }
        
        private async Task<IActionResult> CreateTagInternal(TagCreateDto dto)
        {
            try
            {
                var tag = await _tagService.CreateTagAsync(dto);
                return Ok(tag);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while creating the tag", details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _tagService.GetAllTagsAsync();
            return Ok(tags);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchTags([FromQuery] TagSearchDto searchDto)
        {
            var tags = await _tagService.SearchTagsAsync(searchDto);
            return Ok(tags);
        }

        [HttpPut("{id}/close")]
        public async Task<IActionResult> CloseTag(int id, [FromBody] CloseTagDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var tag = await _tagService.CloseTagAsync(id, dto);
                return Ok(tag);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while closing the tag", details = ex.Message });
            }
        }
    }
}
