using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Back.Controllers
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
            _logger.LogInformation("Back-SuccessRequested");

            await Task.Delay(1500);

            _logger.LogInformation("Back-SuccessResponse");
        }

        [HttpGet("failure")]
        public void Failure()
        {
            _logger.LogInformation("Back-FailureRequested");
            throw new Exception("Test exception");
        }
    }
}
