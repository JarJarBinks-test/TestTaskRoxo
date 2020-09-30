using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace TestTaskRoxo.Front.Filters
{
    public class LogExceptionFilter : IAsyncExceptionFilter
    {
        readonly ILogger<LogActionFilter> logger;
        public LogExceptionFilter(ILogger<LogActionFilter> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            logger.LogInformation($@"{nameof(context.Exception)}. {nameof(context.Exception.Message)}:{context.Exception.Message}, {
                nameof(context.Exception.StackTrace)}:{context.Exception.StackTrace}");
            return Task.CompletedTask;
        }
    }
}
