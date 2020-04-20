using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ej_filters.api.Filters
{
    public class ExampleExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ContentResult()
            {
                StatusCode = 500,
                Content = "Se lanzo una excepcion con el siguiente mensaje: " + context.Exception.Message
            };
        }
    }
}