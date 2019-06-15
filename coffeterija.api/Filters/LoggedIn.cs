using System;
using coffeterija.api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace coffeterija.api.Filters
{
    public class LoggedIn : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {}

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var service = context.HttpContext.RequestServices.GetService<ILoginService>();
            var user = service.MaybeGetUser();
            if(user == null)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
