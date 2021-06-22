using ArpaMedia.Web.Api.Entity.EntityServices;
using ArpaMedia.Web.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscribeController: ControllerBase
    {   
        private ISubscribeService subscribeService;
        private AMResponseProvider responseProvider;
        public SubscribeController()
        {
            this.subscribeService = new SubscribeService();
            this.responseProvider = new AMResponseProvider();
        }

             /**
             * ============================================
             *              PUBLIC APIS
             * ============================================
             **/
        /// <summary>
        /// Public API Create Subscribe
        /// </summary>
        /// <remarks>
        /// 
        /// This request is for create Subscribe.
        ///     
        /// </remarks>
        ///
        /// 
        /// Cases
        /// 
        ///<summary> 
        /// If Email is Exists: api.subscribe.emailExists key.  
        /// </summary>
        /// <param name="request">The object for subscription.</param>
        /// <response code="201">If request model is correct.</response>
        /// <response code="400">If request model is incorrect.</response>
        /// <response code="422">If unprocessable entity.</response>
        [HttpPost("add")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OkResponse<String>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(BadResponse))]
        public IActionResult AddSubscribe([Required] SubscribeRequest request)
        {
            ResponseBase response = this.subscribeService.CreateSubscribe(request);
            return this.responseProvider.VerifyResponse(response, this);
        }

    }
}
