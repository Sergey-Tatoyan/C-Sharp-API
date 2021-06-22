using ArpaMedia.Web.Api.Entity.EntityServices;
using ArpaMedia.Web.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private IConfiguration configuration;
        public TestController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

         /**
         * ============================================
         *              PUBLIC APIS
         * ============================================
         **/

        /**
        * TODO: remove this function
        **/
        [HttpPost("test")]
        public async Task<IActionResult> SendEmailsToSubscribersForPost()
        {
            try
            {
                PostResponse postRequest = new PostResponse();
                postRequest.Title = "testing title";
                postRequest.Description = "yeeeee";
                await (new MailProvider()).SendEmailsToSubscribersForPost(postRequest, this.configuration);
                return Ok("exaaav");
            }
            catch (Exception ex)
            {
                return Ok("chexaaav");
            }

        }
    }
}
