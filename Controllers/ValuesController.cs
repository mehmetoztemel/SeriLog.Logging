using Microsoft.AspNetCore.Mvc;

namespace SeriLog.Logging.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        const string MESSAGE = "API is working";
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(MESSAGE);
        }

        [HttpPost]
        public IActionResult Post(Item request)
        {
            return Ok(MESSAGE);
        }

        [HttpGet("{param}")]
        public IActionResult GetRouteParam(string param)
        {
            return Ok(MESSAGE);
        }
        [HttpGet]
        public IActionResult GetQueryParam(string param)
        {
            return Ok(MESSAGE);
        }
    }


    public class Item
    {
        public string Name { get; set; } = string.Empty;
    }
}
