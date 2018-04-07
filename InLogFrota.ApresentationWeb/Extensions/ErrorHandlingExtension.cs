using InLogFrota.ApresentationWeb.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace InLogFrota.ApresentationWeb.Extensions
{
    public static class ErrorHandlingExtension
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
