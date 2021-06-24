using Common;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using System.Threading.Tasks;

namespace Front.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ITracer _tracer;

        public ApiController(ITracer tracer)
        {
            _tracer = tracer;
        }

        [HttpGet("success")]
        public async Task Success()
        {
            _tracer.ActiveSpan.Log("Front-SuccessRequested");

            var resp = await Tools.CallApi("Middle", "Success", _tracer);

            _tracer.ActiveSpan.Log($"Front-SuccessResponse: {resp.StatusCode}");
        }

        [HttpGet("failure")]
        public async Task Failure()
        {
            _tracer.ActiveSpan.Log("Front-FailureRequested");

            var resp = await Tools.CallApi("Middle", "Failure", _tracer);

            _tracer.ActiveSpan.Log($"Front-FailureResponse: {resp.StatusCode}");
        }
    }
}
