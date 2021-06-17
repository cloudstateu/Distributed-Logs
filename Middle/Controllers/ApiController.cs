using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Middle.Controllers
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
            _logger.LogInformation("Middle-SuccessRequested");

            await Task.Delay(700);
            var resp = await Tools.CallApi("Back", "Success");

            _logger.LogInformation($"Middle-SuccessResponse: {resp.StatusCode}");
        }

        [HttpGet("failure")]
        public async Task Failure()
        {
            _logger.LogInformation("Middle-FailureRequested");
            var resp = await Tools.CallApi("Back", "Failure");
        }
    }
}
