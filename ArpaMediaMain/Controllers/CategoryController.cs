using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.Controllers;
using ArpaMedia.Web.Api.EntityServices;
using ArpaMedia.Web.Api.Models;
using ArpaMedia.Web.Api.Models.Authorization;
using ArpaMedia.Web.Api.Services;
using ArpaMedia.Web.Api.ValidateModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.EntityServices;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private ICategoryService categoryService;
        private AMResponseProvider responseProvider;
        public CategoryController()
        {
            this.categoryService = new CategoryService();
            this.responseProvider = new AMResponseProvider();
        }

        /// <summary>
        /// Create Category
        /// </summary>
        /// <remarks>
        /// This request is for create category.
        /// 
        /// Cases
        /// 
        ///<summary> 
        /// If ParentCategoryId is equal to Caregory Id send error message with <strong>Id</strong> key  
        /// </summary>    
        /// <summary>
        /// If ParentCategoryId is not null program checks if there is category with ParentCategoryId in database. If not returns error with <strong>ParentCategoryId</strong> key.
        /// </summary>
        /// <summary>
        /// If Name is duplicate program sends error message with <strong>Name</strong> key.
        /// </summary>
        ///     
        /// </remarks>
        /// <param name="request">The category object.</param>
        /// <response code="201">If request model is correct.</response>
        /// <response code="400">If request model is incorrect.</response>
        /// <response code="401">If not authorized.</response>
        /// <response code="422">If unprocessable entity.</response>
        [Authorize]
        [HttpPost("add")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OkResponse<CategoryResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(BadResponse))]
        public IActionResult AddCategory([Required] CategoryRequest request)
        {
            ResponseBase response = this.categoryService.CreateCategory(request);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /// <summary>
        /// Update Category
        /// </summary>
        /// <remarks>
        /// This request is for update category.
        /// 
        /// Cases
        /// 
        /// <summary> 
        /// If ParentCategoryId is equal to Caregory Id send error message with <strong>Id</strong> key  
        /// </summary>    
        /// <summary>
        /// If ParentCategoryId is not null program checks if there is category with ParentCategoryId in database. If not returns error with <strong>ParentCategoryId</strong> key.
        /// </summary>
        /// <summary>
        /// If Name is duplicate program sends error message with <strong>Name</strong> key.
        /// </summary>
        ///     
        /// </remarks>
        /// <param name="request">The category model.</param>
        /// <response code="200">If request model is correct.</response>
        /// <response code="400">If request model is incorrect.</response>
        /// <response code="401">If not authorized.</response>
        /// <response code="422">If unprocessable entity.</response>
        [Authorize]
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<CategoryResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(BadResponse))]
        public IActionResult UpdateCategory(CategoryRequest request)
        {
            var response = this.categoryService.UpdateCategory(request);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <remarks>
        /// This request is for delete category.
        /// </remarks>
        /// <param name="request">The category id.</param>
        /// <response code="200">If category id is correct.</response>
        /// <response code="400">If category id is incorrect.</response>
        /// <response code="401">If not authorized.</response>
        [Authorize]
        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        public IActionResult DeleteCategory(DeleteRequest request)
        {
            var response = this.categoryService.DeleteCategory(request);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <remarks>
        /// This request is for getting all categories with their subcategories.
        /// </remarks>
        /// <response code="200">if authorized.</response>
        /// <response code="401">If not authorized.</response>
        [Authorize]
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<List<CategoryResponse>>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        public IActionResult GetAll()
        {
            var response = this.categoryService.GetAll();
            return this.responseProvider.VerifyResponse(response, this);
        }

        /// <summary>
        /// Get child categories by categoryId
        /// </summary>
        /// <remarks>
        /// This request is for getting child categories by categoryId.
        /// </remarks>
        /// <response code="200">if authorized.</response>
        /// <response code="401">If not authorized.</response>
        [Authorize]
        [HttpGet("GetCategoryChildren")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<List<CategoryResponse>>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BadResponse))]
        public IActionResult GetCategoryChildren([FromQuery] int CategoryId)
        {
            var response = this.categoryService.GetCategoryChildren(CategoryId);
            return this.responseProvider.VerifyResponse(response, this);
        }

        /**
         * ============================================
         *              PUBLIC APIS
         * ============================================
         **/

        /// <summary>
        /// Public API Get Menu
        /// </summary>
        /// <remarks>
        /// This request is for getting all menu categories with their subcategories.   
        /// </remarks>
        /// <response code="200">Ok</response>
        [HttpGet("menu")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponse<List<CategoryResponse>>))]
        public IActionResult GetMenu()
        {
            var response = this.categoryService.GetMenu();
            return this.responseProvider.VerifyResponse(response, this);
        }

    }
}
