using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.CGC.WebAPI.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public ExceptionFilter(IHostingEnvironment env)
        {
            this.hostingEnvironment = env;
        }

        public void OnException(ExceptionContext context)
        {
            var exp = new Models.Exception();
            if (this.hostingEnvironment.IsDevelopment())
            {
                exp.Message = context.Exception.Message;
                exp.Details = context.Exception.StackTrace;
            }
            else
            {
                exp.Message = "Server Error";
                exp.Details = context.Exception.Message;
            }

            context.Result = new ObjectResult(exp)
            {
                StatusCode = 500
            };
        }
    }
}
