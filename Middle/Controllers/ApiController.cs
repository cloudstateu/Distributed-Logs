using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OpenTracing;
using System.Threading.Tasks;

namespace Middle.Controllers
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
            _tracer.ActiveSpan?.Log("Middle-SuccessRequested"); ;

            await Delay();
            var resp = await Tools.CallApi("Success", _configuration, _tracer);

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
            
            var resp = await Tools.CallApi("Failure", _configuration, _tracer);

            _tracer.ActiveSpan.Log($"Middle-FailureResponse: {resp.StatusCode}");
        }
    }
}
