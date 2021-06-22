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
    public class BannerTypeController:ControllerBase
    {
        private IBannerTypeService bannerTypeService;
        private AMResponseProvider responseProvider;
        public BannerTypeController()
        {
            this.bannerTypeService = new BannerTypeService();
            this.responseProvider = new AMResponseProvider();
        }

        /// <summary>
        /// Create BannerType
        /// </summary>
        /// <remarks>
        /// This request is for create BannerType.
        /// 
        /// Cases
        ///   
        /// <summary>
        /// If Title is duplicate : Duplicate title.
        /// </summary>
        ///     
        /// </remarks>
        /// <param name="request">The bannerType object.</param>
        /// <response code="201">If request model is correct.</response>
        /// <response code="400">If request model is incorrect.</response>
        /// <response code="401">If not authorized.</response>
        /// <response code="422">If unprocessable entity.</response>
        [Authorize]
        [HttpPost("add")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OkResponse<BannerTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(BadResponse))]
        public IActionResult AddBannerType([Required] BannerTypeRequest request)
        {
            ResponseBase response = this.bannerTypeService.CreateBannerType(request);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /// <summary>
        /// Update BannerType
        /// </summary>
        /// <remarks>
        /// This request is for update bannerTYpe.
        /// 
        /// Cases
        ///
        /// <summary>
        /// If BannerTypeId is null : BannerType was not found.
        /// </summary>
        /// <summary>
        /// If LanguageId is null : Wrong language Id.
        /// </summary>
        /// <summary>
        /// If there is no items in database for given bannerTypeId : BannerType was not found.
        /// </summary>
        /// <summary>
        /// If Title is duplicate : Duplicate title.
        /// </summary>
        ///     
        /// </remarks>
        /// <param name="request">The bannerType model.</param>
        /// <response code="200">If request model is correct.</response>
        /// <response code="400">If request model is incorrect.</response>
        /// <response code="401">If not authorized.</response>
        /// <response code="422">If unprocessable entity.</response>
        [Authorize]
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<BannerTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(BadResponse))]
        public IActionResult UpdateBannerType(BannerTypeRequest request)
        {
            var response = this.bannerTypeService.UpdateBannerType(request);
            return this.responseProvider.VerifyResponse(response, this);
        }
        /// <summary>
        /// Delete BannerType
        /// </summary>
        /// <remarks>
        /// This request is for delete BannerType.
        ///          
        /// Cases
        /// 
        /// <summary> 
        /// If there is no BannerType with given Id : BannerType was not found.  
        /// </summary>    
        ///
        /// </remarks>
        /// <param name="request">The bannerType id.</param>
        /// <response code="200">If bannerType id is correct.</response>
        /// <response code="400">If bannerType id is incorrect.</response>
        /// <response code="401">If not authorized.</response>
        [Authorize]
        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        public IActionResult DeleteBannerType(DeleteRequest request)
        {
            var response = this.bannerTypeService.DeleteBannerType(request);
            return this.responseProvider.VerifyResponse(response, this);
        }
        /// <summary>
        /// Get BannerType by  id.
        /// </summary>
        /// <remarks>
        /// This request is for getting bannerType by given  id.
        ///          
        /// Cases
        /// 
        /// <summary> 
        /// If BannerTypeId is null: BannerType was not found..  
        /// </summary>    
        ///
        /// </remarks>
        /// <param name="BannerTypeId">The bannerType type id.</param>
        /// <response code="200">If bannerType id is correct.</response>
        /// <response code="400">If bannerType id is incorrect.</response>
        /// <response code="401">If not authorized.</response>
        
        [Authorize]
        [HttpGet("{BannerTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<BannerTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        public IActionResult GetBannerTypeById(int BannerTypeId)
        {
            var response = this.bannerTypeService.GetBannerTypeById(BannerTypeId);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /// <summary>
        /// Get BannerType by  languageCode.
        /// </summary>
        /// <remarks>
        /// This request is for getting bannerType by given  languageCode.
        ///          
        /// Cases
        /// 
        /// <summary> 
        /// If BannerTypeLanguageCode is null: Wrong Language Code.  
        /// </summary>    
        /// <summary> 
        /// If BannerTypeId is null: BannerType was not found.  
        /// </summary> 
        /// </remarks>
        /// <param name="LanguageCode">The bannerType type languageCode.</param>
        /// <response code="200">If bannerType languageCode is correct.</response>
        /// <response code="400">If bannerType languageCode is incorrect.</response>
        /// <response code="401">If not authorized.</response>
        [Authorize]
        [HttpGet("language/{LanguageCode}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<BannerTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        public IActionResult GetBannerTypeByLanguageCode(string LanguageCode)
        {
            var response = this.bannerTypeService.GetbannerTypeByLanguageCode(LanguageCode);
            return this.responseProvider.VerifyResponse(response, this);
        }

         /**
         * ============================================
         *              PUBLIC APIS
         * ============================================
         **/
        /// <summary>
        /// Get BannerType by  Title Public API.
        /// </summary>
        /// <remarks>
        /// This request is for getting bannerType by given  Title.
        ///          
        /// Cases
        /// 
        /// <summary> 
        /// If BannerType Title is null: BannerType was not found key..  
        /// </summary>    
        ///
        /// </remarks>
        /// <param name="bannerTypeTitle">The bannerType by Title.</param>
        /// <response code="200">If bannerType Title is correct.</response>
        /// <response code="400">If bannerType Title is incorrect.</response>
        /// <response code="401">If not authorized.</response>

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<BannerTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        public IActionResult GetBannerTypeByTitle([FromQuery] string bannerTypeTitle)
        {
            var response = this.bannerTypeService.GetBannerTypeByTitle(bannerTypeTitle);
            return this.responseProvider.VerifyResponse(response, this);
        }

    }
}
