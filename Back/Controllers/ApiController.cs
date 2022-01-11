using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly DatabaseContext _databaseContext;

        public ApiController(ITracer tracer, DatabaseContext databaseContext)
        {
            _tracer = tracer;
            _databaseContext = databaseContext;
        }

        [HttpGet("success")]
        public async Task Success()
        {
            _tracer.ActiveSpan.Log("Back-SuccessRequested");

            var delayTask = Delay(_tracer.ActiveSpan.Context);
            var dbTask = CallDb();

            await Task.WhenAll(delayTask, dbTask);

            _tracer.ActiveSpan.Log("Back-SuccessResponse");
        }

        // Simulating passing context from external service
        private async Task Delay(ISpanContext parentSpanContext)
        {
            var currentSpan = _tracer.BuildSpan("back-delay")
                .AsChildOf(parentSpanContext)
                .StartActive();

            await Task.Delay(1500);

            currentSpan.Span.Finish();
            currentSpan.Dispose();
        }

        private async Task CallDb()
        {
            if (await _databaseContext.CanConnect())
            {
                await _databaseContext.ExectureQuery();
            }
        }

        [HttpGet("failure")]
        public void Failure()
        {
            _tracer.ActiveSpan.Log("Back-FailureRequested");

            throw new Exception("Test exception");
        }
    }
}
