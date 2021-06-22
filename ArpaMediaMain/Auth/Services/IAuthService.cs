using ArpaMedia.Web.Api.Models;
using Microsoft.Extensions.Configuration;
using WebApi.Models;

namespace WebApi.EntityServices
{
    public interface IAuthService
    {
        ResponseBase Authenticate(AuthenticateRequest model, IConfiguration configuration);
    }
}