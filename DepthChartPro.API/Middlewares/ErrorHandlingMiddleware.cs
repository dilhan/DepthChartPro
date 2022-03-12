using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace DepthChartPro.API.Middlewares
{
    public class ErrorHandlingMiddleware : ExceptionFilterAttribute
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            // Some logic to handle specific exceptions
            var errorMessage = context.Exception is ArgumentException
                ? "ArgumentException occurred"
                : "Some unknown error occurred";

            // Maybe, logging the exception
            _logger.LogError(context.Exception, errorMessage);

            // Returning response
            context.Result = new BadRequestResult();
        }
    }
}
