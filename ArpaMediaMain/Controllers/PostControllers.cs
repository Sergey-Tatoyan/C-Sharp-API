using ArpaMedia.Web.Api.EntityServices;
using ArpaMedia.Web.Api.Models;
using ArpaMedia.Web.Api.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    public class PostController: ControllerBase
    {
        private IPostService postService;
        private AMResponseProvider responseProvider;
        private IConfiguration configuration;

        public PostController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.postService = new PostService();
            this.responseProvider = new AMResponseProvider();
        }

        /// <summary>
        /// Create Post
        /// </summary>
        /// <remarks>
        /// This request is for create post.
        /// 
        /// Cases
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OkResponse<PostResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(BadResponse))]
        public async Task<IActionResult> AddPost([Required] PostRequest request)
        {
            ResponseBase response = await this.postService.CreatePost(request, this.configuration);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /// <summary>
        /// Update Post
        /// </summary>
        /// <remarks>
        /// This request is for update Post.
        /// 
        /// Cases
        /// 
        /// </remarks>
        /// <param name="request">The Post model.</param>
        /// <response code="200">If request model is correct.</response>
        /// <response code="400">If request model is incorrect.</response>
        /// <response code="401">If not authorized.</response>
        /// <response code="422">If unprocessable entity.</response>
        [Authorize]
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<PostResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(BadResponse))]
        public IActionResult UpdatePost(PostRequest request)
        {
            var domain = this.responseProvider.ProcessDomain(this.HttpContext.Request);
            var response = this.postService.UpdatePost(domain, request);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /// <summary>
        /// Delete Post
        /// </summary>
        /// <remarks>
        /// This request is for delete post.
        /// </remarks>
        /// <param name="request">The Post id.</param>
        /// <response code="200">If post id is correct.</response>
        /// <response code="400">If post id is incorrect.</response>
        /// <response code="401">If not authorized.</response>
        [Authorize]
        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        public IActionResult DeletePost(DeleteRequest request)
        {
            var response = this.postService.DeletePost(request);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /// <summary>
        /// Upload Image
        /// </summary>
        /// <remarks>
        /// This request is for upload image for post.
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
        /// If problem with post id: Incorrect Post Id.
        /// </summary>
        ///     
        /// </remarks>
        /// <response code="200">If post id is correct.</response>
        /// <response code="400">If wrong information provided.</response>
        /// <response code="401">If not authorized.</response>
        [Authorize]
        [HttpPost("{postId}/image")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        public IActionResult UploadImage(int postId)
        {
            ResponseBase response = this.postService.UploadImage(postId, HttpContext.Request.Body);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /**
         * ============================================
         *              PUBLIC APIS
         * ============================================
         **/

        /// <summary>
        /// Public API Search Post
        /// </summary>
        /// <remarks>
        /// This request is for search for post.
        ///
        /// </remarks>
        /// <response code="200">If post id is correct.</response>
        /// <response code="400">If wrong information provided.</response>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<List<PostResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        public IActionResult SearchPostForDomain([FromQuery] PostSearchRequest request)
        {
            var domain = this.responseProvider.ProcessDomain(this.HttpContext.Request);
            var response = this.postService.SearchPostForDomain(domain, request);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /// <summary>
        /// Public API Get Posts by CategoryId.
        /// </summary>
        /// <remarks>
        /// This request is for getting  posts.
        /// 
        /// Cases
        ///   
        /// <summary>
        /// If categoryId is wrong: Returnes wrong_Category Id. 
        /// </summary> 
        /// 
        /// </remarks>
        /// <response code="200">If post id is correct.</response>
        /// <response code="400">If wrong information provided.</response>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<List<PostResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        public IActionResult GetPostsWithDomain([FromQuery] PostItemRequest request)
        {
            var domain = this.responseProvider.ProcessDomain(this.HttpContext.Request);
            var response = this.postService.GetPostsWithDomain(domain, request);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /// <summary>
        /// Public API Get Calendar Items.
        /// </summary>
        /// <remarks>
        /// 
        /// This request is for getting calendar items.
        /// Cases
        ///   
        /// <summary>
        /// If language is wrong language: Returnes wrong_language error code. 
        /// </summary> 
        /// 
        /// </remarks>
        /// <response code="200">If category and language are correct.</response>
        /// <response code="400">If wrong information provided.</response>
        [HttpGet("calendarItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<List<PostResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        public IActionResult GetCalendarItemsWithDomain([FromQuery] CalendarItemsRequest request)
        {
            var domain = this.responseProvider.ProcessDomain(this.HttpContext.Request);
            var response = this.postService.GetCalendarItemsWithDomain(domain, request);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /// <summary>
        /// Public API Get Home Posts replace imageURLs.
        /// </summary>
        /// <remarks>
        /// 
        /// This request is for getting Home Posts from Categories.
        /// Cases
        ///   
        /// <summary>
        /// If language is wrong language: Returnes wrong_language error code. 
        /// </summary> 
        /// 
        /// </remarks>
        /// <response code="200">If category and language are correct.</response>
        /// <response code="400">If wrong information provided.</response>
        [HttpGet("home")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<List<HomeItemsResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        public IActionResult GetHomeItemsWithDomain([FromQuery] HomeItemsRequest request)
        {
            var domain = this.responseProvider.ProcessDomain(this.HttpContext.Request);
            var response = this.postService.GetHomeItemsWithDomain(domain, request);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /// <summary>
        /// Public API Get Post By Id
        /// </summary>
        /// <remarks>
        /// 
        /// This request is for getting  Posts by id.
        /// Cases
        ///   
        /// <summary>
        /// If post is not found: Wrong post Id. 
        /// </summary> 
        /// 
        /// </remarks>
        /// <response code="200">If post Id correct.</response>
        /// <response code="400">If wrong post id.</response>
        [HttpGet("{PostId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<PostResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        public IActionResult GetPostById(int PostId)
        {
            var domain = this.responseProvider.ProcessDomain(this.HttpContext.Request);
            var response = this.postService.GetPostByIdWithDomain(domain, PostId);
            return this.responseProvider.VerifyResponse(response, this);
        }
    }
}
