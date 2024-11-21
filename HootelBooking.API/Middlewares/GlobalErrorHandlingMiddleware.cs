using HootelBooking.Application.Exceptions;
using Newtonsoft.Json;
using Serilog;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace HootelBooking.API.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var controllerName = context.GetRouteData().Values["controller"]?.ToString();
                var actionName = context.GetRouteData().Values["action"]?.ToString();
                Log.Information("Trying to execute: {controllerName}/{ActionName}", controllerName, actionName);

                await _next(context);

                Log.Information("{controllerName}/{ActionName}: executed successfully\n", controllerName, actionName);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // Prepare the response 
            context.Response.ContentType = "application/json";

            // Default to Internal Server Error
            int statusCode = GetStatusCode(ex);
            string errorMessage = "An error occurred while processing your request.";
            string[] errorDetails = new[] { ex.Message };

            if (ex is ErrorResponseException customException)
            {
                // Use the properties from the ErrorResponseException
                statusCode = customException.StatusCode;
                errorMessage = customException.Message; // You can customize this further if needed
                errorDetails = new[] { customException.Error }; // Return the custom error message
            }
           
            else if (ex is FluentValidation.ValidationException validationException)
            {
                errorMessage = "Validation Errors!!";
                statusCode = (int)HttpStatusCode.BadRequest;
                errorDetails = validationException.Errors.Select(e => e.ErrorMessage).ToArray();

            }

            // Set the status code in the response
            context.Response.StatusCode = statusCode;

            // Create error response
            var result = CreateErrorResponse(statusCode, errorMessage, errorDetails);
            return context.Response.WriteAsync(result);
        }

        private string CreateErrorResponse(int statusCode, string message, string[] details)
        {
            var errorDetails = new
            {
                StatusCode = statusCode,
                Message = message,
                Detail = details,
                TimeStamp = DateTime.UtcNow
            };

            return JsonConvert.SerializeObject(errorDetails);
        }

        private int GetStatusCode( Exception ex)
        {

            return ex switch
            {
                UnAuthorizeException => StatusCodes.Status401Unauthorized,    // 401 Unauthorized
                ForbiddenException => StatusCodes.Status403Forbidden,               // 403 Forbidden  
                _ => StatusCodes.Status500InternalServerError                     
            };
        }
    }
}
