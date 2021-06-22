using ArpaMedia.Web.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArpaMedia.Web.Api.Controllers
{
    public class AMResponseProvider
    {
        public IActionResult VerifyResponse(ResponseBase response, ControllerBase controllerBase)
        {
            switch (response.StatusCode)
            {
                case StatusCodes.Status201Created: 
                    return controllerBase.Created("add", response);
                case StatusCodes.Status200OK:
                    return controllerBase.Ok(response);
                default:
                    return controllerBase.BadRequest(response); 
            }
        }

        public string ProcessDomain(HttpRequest request)
        {
            var domain = request.Scheme.ToString() + @"//" + request.Host.ToString() + @"/";
            return domain;
        }
    }
}
