using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BrightCare.Web.Api.Middleware
{
    /// <summary>
    /// Handles exception across the application
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Used to inject dependencies
        /// </summary>
        /// <param name="next"></param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //await HandleExceptionAsync(httpContext, ex);
            }
        }

        //private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        //{
        //    var code = HttpStatusCode.InternalServerError;

        //    var errors = JsonConvert.SerializeObject(exception.Message);
        //    var message = "Internal Server Error";

        //    switch (exception)
        //    {
        //        case ValidationException validationException:
        //            code = HttpStatusCode.BadRequest;
        //            errors = JsonConvert.SerializeObject(validationException.Failures);
        //            message = "Bad Request";
        //            break;
        //        case NotFoundException _:
        //            code = HttpStatusCode.NotFound;
        //            message = "Not Found";
        //            break;
        //        case InvalidResponseException invalidResponse:
        //            code = HttpStatusCode.InternalServerError;
        //            errors = JsonConvert.SerializeObject(invalidResponse.Message);
        //            message = "Server Error";
        //            break;
        //        case InvalidRequestException invalidRequest:
        //            code = HttpStatusCode.BadRequest;
        //            errors = JsonConvert.SerializeObject(invalidRequest.Message);
        //            message = "Bad Request";
        //            break;
        //    }

        //    context.Response.ContentType = "application/json";
        //    context.Response.StatusCode = (int)code;

        //    var responseError = new ErrorDetailsDto()
        //    {
        //        StatusCode = context.Response.StatusCode,
        //        Message = message,
        //        Details = errors
        //    };
        //    await context.Response.WriteAsync(JsonConvert.SerializeObject(responseError));
        //}
    }
}
