using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace TestTaskRoxo.Front.Filters
{
    public class LogActionFilter : IAsyncActionFilter
    {
        readonly ILogger<LogActionFilter> logger;
        public LogActionFilter(ILogger<LogActionFilter> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var request = context.HttpContext.Request;
            logger.LogInformation($"Action. {nameof(request.Path)}: {context.HttpContext.Request.Path}, {nameof(request.QueryString)}: {request.QueryString}");
            return next();
        }
    }
}
