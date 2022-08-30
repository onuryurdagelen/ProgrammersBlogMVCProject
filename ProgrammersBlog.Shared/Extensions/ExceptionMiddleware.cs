using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next; 
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
        //public Task HandleExceptionAsync(HttpContext httpContext,Exception ex)
        //{
        //    httpContext.Response.ContentType = "application/json";
        //    httpContext.Response.StatusCode = (int)httpContext.Response.StatusCode;

        //    switch (ex)
        //    {
        //        case

        //        default:
        //            break;
        //    }


        //}

    }
}
