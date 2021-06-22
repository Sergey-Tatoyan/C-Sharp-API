using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (User)context.HttpContext.Items["User"];
        if (user == null)
        {
            // not logged in
            BadResponse response = new BadResponse();
            response.StatusCode = 401;
            response.AddResponseError(new string[] { "Unauthorized" });
            context.Result = new JsonResult(response) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}