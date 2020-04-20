using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ej_filters.api.Filters
{
    public class ExampleActionFilter : Attribute, IActionFilter
    {
        private Stopwatch timer;
        public void OnActionExecuting(ActionExecutingContext context)
        {
            timer = Stopwatch.StartNew();
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            timer.Stop();
            string result = " Tiempo de ejecucion: " + $"{timer.Elapsed.TotalMilliseconds} ms";
            ((ObjectResult)context.Result).Value = result;
        }
    }
}