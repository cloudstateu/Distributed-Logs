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

            await Delay();
            var resp = await Tools.CallApi("Back", "Success", _tracer);

            _tracer.ActiveSpan.Log($"Middle-SuccessResponse: {resp.StatusCode}");
        }

        private async Task Delay()
        {
            var currentStpan = _tracer.BuildSpan("middle-delay")
                .AsChildOf(_tracer.ActiveSpan)
                .StartActive();

            await Task.Delay(700);

            currentStpan.Span.Finish();
            currentStpan.Dispose();
        }

        [HttpGet("failure")]
        public async Task Failure()
        {
            _tracer.ActiveSpan.Log("Middle-FailureRequested");
            
            var resp = await Tools.CallApi("Back", "Failure", _tracer);

            _tracer.ActiveSpan.Log($"Middle-FailureResponse: {resp.StatusCode}");
        }
    }
}
