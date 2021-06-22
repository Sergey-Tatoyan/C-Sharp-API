using ArpaMedia.Web.Api.Entity.EntityServices;
using ArpaMedia.Web.Api.EntityServices;
using ArpaMedia.Web.Api.Models;
using ArpaMedia.Web.Api.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LanguageController : ControllerBase
    {
        private ILanguageService languageService;
        private AMResponseProvider responseProvider;
        public LanguageController()
        {
            this.languageService = new LanguageService();
            this.responseProvider = new AMResponseProvider();
        }

        /// <summary>
        /// Create Language
        /// </summary>
        /// <remarks>
        /// 
        /// This request is for create language.
        ///     
        /// </remarks>
        /// <param name="request">The post object.</param>
        /// <response code="201">If request model is correct.</response>
        /// <response code="400">If request model is incorrect.</response>
        /// <response code="401">If not authorized.</response>
        /// <response code="422">If unprocessable entity.</response>
        [Authorize]
        [HttpPost("add")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OkResponse<LanguageResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(BadResponse))]
        public IActionResult AddPost([Required] LanguageRequest request)
        {
            ResponseBase response = this.languageService.CreateLanguage(request);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /// <summary>
        /// Update Language
        /// </summary>
        /// <remarks>
        /// This request is for update Language.
        /// 
        /// Cases
        /// 
        /// <summary> 
        /// If languageId is null: Wrong Language Id.  
        /// </summary>    
        ///
        /// </remarks>
        /// <param name="request">The Language model.</param>
        /// <response code="200">If request model is correct.</response>
        /// <response code="400">If request model is incorrect.</response>
        /// <response code="401">If not authorized.</response>
        /// <response code="422">If unprocessable entity.</response>
        [Authorize]
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<LanguageResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(BadResponse))]
        public IActionResult UpdateLanguage(LanguageRequest request)
        {
            var response = this.languageService.UpdateLanguage(request);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /**
         * ============================================
         *              PUBLIC APIS
         * ============================================
         **/

        /// <summary>
        /// Public API Get Languages
        /// </summary>
        /// <remarks>
        /// 
        /// This request is for getting languages.
        ///    
        /// </remarks>
        /// <response code="400">If request model is incorrect.</response>
        /// <response code="422">If unprocessable entity.</response>
        [HttpGet("all")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<List<LanguageResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(BadResponse))]
        public IActionResult AllLanguages()
        {
            ResponseBase response = this.languageService.GetLanguages();
            return this.responseProvider.VerifyResponse(response, this);
        }
    }
}
