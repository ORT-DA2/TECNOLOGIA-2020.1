using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Moodle.BusinessLogic.Interface;

namespace Moodle.WebApi
{
    public class AuthenticationFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string headerToken = context.HttpContext.Request.Headers["Authorization"];

            if (headerToken is null)
            {
                context.Result = new ContentResult()
                {
                    Content = "Token is required"
                };
            }
            else
            {
                try
                {
                    Guid token = Guid.Parse(headerToken);
                    this.VerifyToken(token, context);
                }
                catch (FormatException)
                {
                    context.Result = new ContentResult()
                    {
                        Content = "Token bad format"
                    };
                }
            }
        }

        private void VerifyToken(Guid token, ActionExecutingContext context)
        {
            var session = this.GetSessionLogic(context);
            if(!session.IsValidToken(token))
            {
                context.Result = new ContentResult()
                {
                    Content = "Invalid Token"
                };
            }
            else
            {
                int userLoggedId = 0;
                context.ActionArguments.Add("userLoggedId", userLoggedId);
            }
        }

        private ISessionLogic GetSessionLogic(ActionExecutingContext context)
        {
            return context.HttpContext.RequestServices.GetService(typeof(ISessionLogic)) as ISessionLogic;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}