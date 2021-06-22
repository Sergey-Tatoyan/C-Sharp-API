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
    public class BannerController:ControllerBase
    {
        private IBannerService bannerService;
        private AMResponseProvider responseProvider;
        public BannerController()
        {
            this.bannerService = new BannerService();
            this.responseProvider = new AMResponseProvider();
        }

        /// <summary>
        /// Create Banner
        /// </summary>
        /// <remarks>
        /// This request is for create banner.
        /// 
        /// Cases
        ///   
        /// <summary>
        /// If BannerTypeId is null : Wrong BannerType Id.
        /// </summary>
        /// <summary>
        /// If Title is duplicate : Duplicate title.
        /// </summary>
        ///     
        /// </remarks>
        /// <param name="request">The banner object.</param>
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
        public IActionResult AddBanner([Required] BannerRequest request)
        {
            ResponseBase response = this.bannerService.CreateBanner(request);
            return this.responseProvider.VerifyResponse(response, this);
        }


        /// <summary>
        /// Update Banner
        /// </summary>
        /// <remarks>
        /// This request is for update banner.
        /// 
        /// Cases
        ///    
        /// <summary>
        /// If BannerId is not null : Banner was not found.
        /// </summary>
        /// <summary>
        /// If BannerTypeId is null : Wrong BannerType Id.
        /// </summary>
        /// <summary>
        /// If Title is duplicate : Duplicate title.
        /// </summary>
        ///     
        /// </remarks>
        /// <param name="request">The banner model.</param>
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
        public IActionResult UpdateBanner(BannerRequest request)
        {
            var response = this.bannerService.UpdateBanner(request);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /// <summary>
        /// Delete Banner
        /// </summary>
        /// <remarks>
        /// This request is for delete Banner.
        ///          
        /// Cases
        /// 
        /// <summary> 
        /// If there is no Banner with given Id : Banner was not found.
        /// </summary>    
        ///
        /// </remarks>
        /// <param name="request">The banner id.</param>
        /// <response code="200">If banner id is correct.</response>
        /// <response code="400">If banner id is incorrect.</response>
        /// <response code="401">If not authorized.</response>
        [Authorize]
        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        public IActionResult DeleteBanner(DeleteRequest request)
        {
            var response = this.bannerService.DeleteBanner(request);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /// <summary>
        /// Get Banners by Banner type.
        /// </summary>
        /// <remarks>
        /// This request is for getting banners by given banner type.
        ///          
        /// Cases
        /// 
        /// <summary> 
        /// If BannerType is null: Wrong BannerType.  
        /// </summary>    
        ///
        /// </remarks>
        /// <param name="bannerType">The banner type.</param>
        /// <response code="200">If banner type is correct.</response>
        /// <response code="400">If banner type is incorrect.</response>
        /// <response code="401">If not authorized.</response> 
        [Authorize]
        [HttpGet("{bannerType}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<List<BannerTypeResponse>>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        public IActionResult GetBannerForBannerType(string bannerType)
        {
            var domain = this.responseProvider.ProcessDomain(this.HttpContext.Request);
            var response = this.bannerService.GetBannerForBannerTypeWithDomain(domain, bannerType);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /// <summary>
        /// Upload Image
        /// </summary>
        /// <remarks>
        /// This request is for upload image for banenr.
        /// 
        /// Cases
        /// 
        ///<summary> 
        /// If image is null: Image can not be null.  
        /// </summary>    
        /// <summary>
        /// If Damiged image: File is not correct image.
        /// </summary>
        /// <summary>
        /// If not png or jpg: Image format is not supported.
        /// </summary>        
        /// <summary>
        /// If image size is bigger than 5mb: Image is to big.
        /// </summary>        
        /// <summary>
        /// If problem with banner id: Incorrect Banner Id.
        /// </summary>
        ///     
        /// </remarks>
        /// <response code="200">If banner id is correct.</response>
        /// <response code="400">If wrong information provided.</response>
        /// <response code="401">If not authorized.</response>
        [Authorize]
        [HttpPost("{bannerId}/image")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        public IActionResult UploadImage(int bannerId)
        {
            ResponseBase response = this.bannerService.UploadImage(bannerId, HttpContext.Request.Body);
            return this.responseProvider.VerifyResponse(response, this);
        }

    }
}
