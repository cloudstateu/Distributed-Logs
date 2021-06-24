using Microsoft.AspNetCore.Mvc.Filters;
using OpenTracing;
using System.Linq;

namespace Common.ActionFilters
{
    public class OpenTracingBaggageFilter : IActionFilter
    {
        private readonly ITracer _tracer;

        public OpenTracingBaggageFilter(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var correlationId = context.HttpContext.Request.Headers["correlation-id"].FirstOrDefault();

            if (!string.IsNullOrEmpty(correlationId))
            {
                _tracer.ActiveSpan.SetBaggageItem("correlation-id", correlationId);
            }
        }
    }
}
