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

            await Task.Delay(1500);

            _tracer.ActiveSpan.Log("Back-SuccessResponse");
        }

        [HttpGet("failure")]
        public void Failure()
        {
            _tracer.ActiveSpan.Log("Back-FailureRequested");

            throw new Exception("Test exception");
        }
    }
}
