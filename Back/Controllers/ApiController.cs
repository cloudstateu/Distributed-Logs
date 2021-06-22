﻿using Microsoft.AspNetCore.Mvc;
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
            
            await Delay();

            _tracer.ActiveSpan.Log("Back-SuccessResponse");
        }

        private async Task Delay()
        {
            var currentSpan = _tracer.BuildSpan("back-delay")
                .AsChildOf(_tracer.ActiveSpan)
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
