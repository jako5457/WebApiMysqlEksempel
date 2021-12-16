using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Pressures;
using ServiceLayer.Dto;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PressureController : ControllerBase
    {

        private readonly IPressureService _pressureService;

        public PressureController(IPressureService pressureService)
        {
            _pressureService = pressureService;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<PressureDto>> Get()
        {
            return await _pressureService.GetAllAsync();
        }

        // GET api/<ValuesController>/5
        [HttpGet]
        [Route("Latest")]
        public async Task<PressureDto> GetLatest()
        {
            return await _pressureService.GetLatestAsync();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task Post([FromBody] double value)
        {
           await _pressureService.SetAsync(value);
        }
    }
}
