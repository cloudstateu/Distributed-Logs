using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using System;
using System.Threading.Tasks;

namespace Back.Controllers
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
            _tracer.ActiveSpan.Log("Back-SuccessRequested");

            // Simulating passing context from external service
            await Delay(_tracer.ActiveSpan.Context);

            _tracer.ActiveSpan.Log("Back-SuccessResponse");
        }

        private async Task Delay(ISpanContext parentSpanContext)
        {
            var currentSpan = _tracer.BuildSpan("back-delay")
                .AsChildOf(parentSpanContext)
                .StartActive();

            await Task.Delay(1500);

            currentSpan.Span.Finish();
            currentSpan.Dispose();
        }

        [HttpGet("failure")]
        public void Failure()
        {
            _tracer.ActiveSpan.Log("Back-FailureRequested");

            throw new Exception("Test exception");
        }
    }
}
