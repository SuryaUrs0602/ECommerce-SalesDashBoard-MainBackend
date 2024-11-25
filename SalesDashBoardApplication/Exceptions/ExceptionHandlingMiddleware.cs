
using Microsoft.EntityFrameworkCore;

namespace SalesDashBoardApplication.Exceptions
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);    
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var (statusCode, message) = exception switch
            {
                InvalidOperationException ioEx => 
                    (StatusCodes.Status400BadRequest, "Bad Request: " + ioEx.Message),

                DbUpdateException dbEx =>
                    (StatusCodes.Status500InternalServerError, "Database Error: " + dbEx.InnerException?.Message ?? "An error occurred while updating the database."),

                UnauthorizedAccessException _ =>
                    (StatusCodes.Status403Forbidden, "Access Denied: You do not have permission to access this resource."),

                OperationCanceledException _ =>
                    (StatusCodes.Status408RequestTimeout, "Request Timeout: The operation was canceled."),

                ArgumentNullException argEx =>
                    (StatusCodes.Status400BadRequest, "Bad Request: Missing argument - " + argEx.ParamName),

                FormatException _ =>
                    (StatusCodes.Status400BadRequest, "Bad Request: The format of the request is invalid."),

                Exception _ =>
                    (StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please try again later.")
            };

            response.StatusCode = statusCode;

            await response.WriteAsJsonAsync(new { error = message });
        }
    }
}
