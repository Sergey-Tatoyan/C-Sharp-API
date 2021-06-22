using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.EntityServices
{
    interface ICategoryService
    {
        /// <summary>
		/// Create Category object with all validations.
		/// </summary>
		/// <param name="request">Necessary data to save in database.</param> 
        /// <returns name="Category">Saved Category Object.</returns> 
        public ResponseBase CreateCategory(CategoryRequest request);

        /// <summary>
        /// Update Category object with all validations.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <returns name="Category">Saved Category Object.</returns>
        public ResponseBase UpdateCategory(CategoryRequest request);

        /// <summary>
        /// Delete Category object in database.
        /// </summary>
        /// <param name="request">Category to delete.</param> 
        /// <returns name="ResponseBase">Status code and message.</returns> 
        public ResponseBase DeleteCategory(DeleteRequest request);

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns name="ResponseBase">Status code, message and all categories.</returns> 
        public ResponseBase GetAll();

        /// <summary>
        /// Get all menues.
        /// </summary>
        /// <returns name="ResponseBase">Status code, message and all menues.</returns> 
        public ResponseBase GetMenu();

        /// <summary>
        /// Get all child categories.
        /// </summary>
        /// <returns name="ResponseBase">Status code, message and all child categories.</returns> 
        public ResponseBase GetCategoryChildren(int id);
    }
}
