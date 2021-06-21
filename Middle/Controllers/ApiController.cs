using Common;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using System.Threading.Tasks;

namespace Middle.Controllers
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
            _tracer.ActiveSpan?.Log("Middle-SuccessRequested"); ;

            await Task.Delay(700);
            var resp = await Tools.CallApi("Back", "Success");

            _tracer.ActiveSpan.Log($"Middle-SuccessResponse: {resp.StatusCode}");
        }

        [HttpGet("failure")]
        public async Task Failure()
        {
            _tracer.ActiveSpan.Log("Middle-FailureRequested");
            
            var resp = await Tools.CallApi("Back", "Failure");

            _tracer.ActiveSpan.Log($"Middle-FailureResponse: {resp.StatusCode}");
        }
    }
}
