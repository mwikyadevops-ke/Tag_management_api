using Microsoft.AspNetCore.Mvc;
using TagManagement.Dtos;
using TagManagement.Services;

namespace TagManagement.Controllers
{
    [ApiController]
    [Route("api/weight-events")]
    public class WeightController : Controller
    {

        private readonly WeighEventService _weighService;

        public WeightController(WeighEventService weighEventService) {
            _weighService = weighEventService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateWeighEvent(WeightEventDto dto)
        {
            var result = await _weighService.CreateWeighEventAsync(dto);
            return Ok(result);
        }
       
    }
}
