using Microsoft.AspNetCore.Mvc.Filters;
using OpenTracing;

namespace Common.ActionFilters
{
    public class OpenTracingTraceIdFilter : IActionFilter
    {
        private readonly ITracer _tracer;

        public OpenTracingTraceIdFilter(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var traceId = _tracer.ActiveSpan?.Context?.TraceId;

            if (!string.IsNullOrEmpty(traceId))
            {
                context.HttpContext.Response.Headers.Add("trace-id", traceId);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            return;
        }
    }
}
