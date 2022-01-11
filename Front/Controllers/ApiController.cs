using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OpenTracing;
using System.Threading.Tasks;

namespace Front.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ITracer _tracer;
        private readonly IConfiguration _configuration;

        public ApiController(ITracer tracer, IConfiguration configuration)
        {
            _tracer = tracer;
            _configuration = configuration;
        }

        [HttpGet("success")]
        public async Task Success()
        {
            _tracer.ActiveSpan.Log("Front-SuccessRequested");

            var resp = await Tools.CallApi("Success", _configuration, _tracer);

            _tracer.ActiveSpan.Log($"Front-SuccessResponse: {resp.StatusCode}");
        }

        [HttpGet("failure")]
        public async Task Failure()
        {
            _tracer.ActiveSpan.Log("Front-FailureRequested");

            var resp = await Tools.CallApi("Middle", _configuration, _tracer);

            _tracer.ActiveSpan.Log($"Front-FailureResponse: {resp.StatusCode}");
        }
    }
}
