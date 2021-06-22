using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.EntityProviders;
using ArpaMedia.Web.Api.Models;
using ArpaMedia.Web.Api.Services.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ArpaMedia.Web.Api.EntityServices
{
    public class CategoryService : BaseEntityService, ICategoryService
    {
        /// <summary>
		/// Create Category object with all validations.
		/// </summary>
		/// <param name="request">Necessary data to save in database.</param> 
        /// <returns name="Category">Saved Category Object.</returns> 
        public ResponseBase CreateCategory(CategoryRequest request)
        {
            Language language = LanguageHelper.GetLanguageById(request.LanguageId, DBArpaContext);
            if (language == null)
            {
                BadResponse badResponse = new BadResponse();
                badResponse.AddResponseError("Id", new string[] { "Wrong Language Id." });
                return badResponse;
            }
            BadResponse response = new BadResponse();
            if (request.ParentCategoryId == 0)
            {
                response.AddResponseError("Id", new string[] { "ParentCategoryId can not be 0." });
                return response;
            }
            if (request.ParentCategoryId == request.Id)
            {
                response.AddResponseError("Id", new string[] { "ParentCategoryId and Category Id can not be equal." });
                return response;
            }
            if (request.ParentCategoryId != null)
            {
                var parent = CategoryHelper.GetCategoryById((int)request.ParentCategoryId, DBArpaContext);
                if (parent == null)
                {
                    response.AddResponseError("ParentCategoryId", new string[] { "Wrong ParentId." });
                    return response;
                }
            }
            var oldCategory = CategoryHelper.GetCategoryByName(request.Name, request.ParentCategoryId, DBArpaContext);
            if (oldCategory != null)
            {
                response.AddResponseError("Name", new string[] { "Duplicate name." });
            }

            if (response.Errors != null)
            {
                return response;
            }

            try
            {
                OkResponse<CategoryResponse> okResponse = new OkResponse<CategoryResponse>();
                var categoryEntity = CategoryHelper.CreateCategory(request, DBArpaContext);
                CategoryResponse categoryResponse = new CategoryResponse(categoryEntity);
                okResponse.Response = categoryResponse;
                okResponse.StatusCode = StatusCodes.Status201Created;
                return okResponse;
            }
            catch (Exception e)
            {
                BadResponse badResponse = new BadResponse();
                badResponse.AddResponseError(new string[] { e.Message });
                return badResponse;
            }
        }

        /// <summary>
        /// Ubdate Category object with all validations.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <returns name="Category">Saved Category Object.</returns> 
        public ResponseBase UpdateCategory(CategoryRequest request)
        {
            Language language = LanguageHelper.GetLanguageById(request.LanguageId, DBArpaContext);
            if (language == null)
            {
                BadResponse badResponse = new BadResponse();
                badResponse.AddResponseError("Id", new string[] { "Wrong Language Id." });
                return badResponse;
            }
            BadResponse response = new BadResponse();
            if (request.ParentCategoryId == request.Id)
            {
                response.AddResponseError("Id", new string[] { "ParentCategoryId and Category Id can not be equal." });
                return response;
            }
            var currentCategory = CategoryHelper.GetCategoryById(request.Id, DBArpaContext);
            if (currentCategory == null)
            {
                response.AddResponseError(new string[] { "Category was not found." });
                return response;
            }
            if (request.ParentCategoryId != null)
            {
                var parent = CategoryHelper.GetCategoryById((int)request.ParentCategoryId, DBArpaContext);
                if (parent == null)
                {
                    response.AddResponseError("ParentCategoryId", new string[] { "Invalid ParentCategoryId." });
                    return response;
                }
            }

            var oldCategory = CategoryHelper.GetCategoryByName(request.Name, request.ParentCategoryId, DBArpaContext);

            if (oldCategory != null && oldCategory.Id != currentCategory.Id)
            {
                response.AddResponseError("Name", new string[] { "Duplicate name." });
            }

            if (response.Errors != null)
            {
                return response;
            }

            try
            {
                OkResponse<CategoryResponse> okResponse = new OkResponse<CategoryResponse>();
                var categoryEntity = CategoryHelper.UpdateCategory(request, DBArpaContext);
                CategoryResponse categoryResponse = new CategoryResponse(categoryEntity);
                okResponse.Response = categoryResponse;
                okResponse.StatusCode = StatusCodes.Status200OK;
                return okResponse;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        /// <summary>
        /// Delete Category object in database.
        /// </summary>
        /// <param name="request">Category to delete.</param> 
        /// <returns name="ResponseBase">Status code and message.</returns> 
        public ResponseBase DeleteCategory(DeleteRequest request)
        {
            Category category = CategoryHelper.GetCategoryById(request.Id, DBArpaContext);
            if (category == null)
            {
                return new BadResponse("Category was not found.");
            }
            if (category.InverseParentCategory.Count != 0)
            {
                return new BadResponse("You can't delete. Please consider removeing subcategories.");
            }

            if (category.CategoryPosts.Count != 0)
            {
                return new BadResponse("You can't delete. Please consider removeing posts connected with this category." );
            }

            OkResponse<Object> okResponse = new OkResponse<Object>();
            CategoryHelper.DeleteCategory(category.Id, DBArpaContext);
            okResponse.StatusCode = StatusCodes.Status200OK;
            okResponse.Response = "Success";
            return okResponse;
        }

        /// <summary>
        /// Get all Categories from database.
        /// </summary>
        /// <returns name="ResponseBase">List of CategoryResponse </returns> 
        public ResponseBase GetAll()
        {
            BadResponse response = new BadResponse();
            response.StatusCode = StatusCodes.Status200OK;
            var categories = CategoryHelper.GetCateogories(DBArpaContext);

            ICollection<CategoryResponse> categoryResponse = new List<CategoryResponse>();
            foreach (var item in categories)
            {
                categoryResponse.Add(ConvertCategoryToCategoryResponse(item, DBArpaContext, false));
            }

            OkResponse<ICollection<CategoryResponse>> okResponsde = new OkResponse<ICollection<CategoryResponse>>();
            okResponsde.Response = categoryResponse;
            return okResponsde;
        }

        /// <summary>
        /// Get all child Categories by categoryId.
        /// </summary>
        /// <param name="CategoryId">CategoryId to find his childCategories.</param> 
        /// <returns name="ResponseBase">List of CategoryResponse.</returns> 
        public ResponseBase GetCategoryChildren(int CategoryId)
        {
            ICollection<Category> childCategories = CategoryHelper.GetCategoryChildren(CategoryId, DBArpaContext);
            ICollection<CategoryResponse> categoryResponse = new List<CategoryResponse>();
            foreach (var item in childCategories)
            {
                categoryResponse.Add(ConvertCategoryToCategoryResponse(item, DBArpaContext, false));
            }

            OkResponse<ICollection<CategoryResponse>> okResponsde = new OkResponse<ICollection<CategoryResponse>>();
            okResponsde.Response = categoryResponse;
            return okResponsde;

            
        }

        /// <summary>
        /// Get  Categories for menu.
        /// </summary> 
        /// <returns name="ResponseBase">List of CategoryResponse.</returns> 
        public ResponseBase GetMenu()
        {
            OkResponse<ICollection<CategoryResponse>> response = new OkResponse<ICollection<CategoryResponse>>();
            var categories = CategoryHelper.GetCateogories(DBArpaContext, true);
            ICollection<CategoryResponse> categoryResponse = new List<CategoryResponse>();
            foreach (var item in categories)
            {
                categoryResponse.Add(ConvertCategoryToCategoryResponse(item, DBArpaContext, true));
            }
            response.Response = categoryResponse;
            return response;
        }

        /// <summary>
        /// Convert Category object to CategoryResponse object.
        /// </summary>
        /// <param name="category">Category to convert.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <param name="isForMenu">Check if require only menu items.</param>
        private static CategoryResponse ConvertCategoryToCategoryResponse(Category category, ArpaMediaContext context, bool isForMenu)
        {
            CategoryResponse categoryResponse = new CategoryResponse();
            categoryResponse.Id = category.Id;
            categoryResponse.ParentCategoryId = category.ParentCategoryId;
            categoryResponse.Name = category.Name;
            categoryResponse.DisplayOrder = category.DisplayOrder;
            categoryResponse.IsMenuItem = category.IsMenuItem;
            categoryResponse.IsHomeItem = category.IsHomeItem;
            categoryResponse.IsCalendarItem = category.IsCalendarItem;
            categoryResponse.ChildCategories = new List<CategoryResponse>();
            categoryResponse.Language = new LanguageResponse(category.Language);
            ICollection<Category> childCategories = CategoryHelper.GetCategoryChildren(category, context, isForMenu);
            foreach (var categoryItem in childCategories)
            {
                categoryResponse.ChildCategories.Add(ConvertCategoryToCategoryResponse(categoryItem, context, isForMenu));
            }
            return categoryResponse;
        }
    }
}
