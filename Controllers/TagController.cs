using Microsoft.AspNetCore.Mvc;
using TagManagement.Dtos;
using TagManagement.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TagManagement.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagController : ControllerBase
    {

        private readonly IItagService _tagService;
        public TagController(IItagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTag (TagCreateDto dto)
        {
            var tag = await _tagService.CreateTagAsync(dto);

            return Ok(tag);
        }


        // GET: api/<TagController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
