using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Front.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {

        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet("success")]
        public async Task Success()
        {
            _logger.LogInformation("Front-SuccessRequested");

            var resp = await Tools.CallApi("Middle", "Success");

            _logger.LogInformation($"Front-SuccessResponse: {resp.StatusCode}");
        }

        [HttpGet("failure")]
        public void Failure()
        {
            _logger.LogInformation("Front-FailureRequested");
            Tools.CallApi("Middle", "Failure");
        }
    }
}
