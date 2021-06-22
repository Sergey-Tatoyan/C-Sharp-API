using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.EntityProviders
{
    public class CategoryHelper
    {
        /// <summary>
		/// Create Category object in database.
		/// </summary>
		/// <param name="request">Necessary data to save in database.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="Category">Saved Category Object.</returns> 
        public static Data.Models.Category CreateCategory(CategoryRequest request, ArpaMediaContext context)
        {
            Category category = new Category();
            category.Name = request.Name;
            category.ParentCategoryId = request.ParentCategoryId;
            category.DisplayOrder = request.DisplayOrder;
            category.LastModified = DateTime.Now;
            category.Created = DateTime.Now;
            category.LanguageId = request.LanguageId;
            category.IsMenuItem = request.IsMenuItem;
            category.IsHomeItem = request.IsHomeItem;
            category.IsCalendarItem = request.IsCalendarItem;
            try
            {
                var savedCategory = context.Add(category);
                context.SaveChanges();
                return savedCategory.Entity;
            }
            catch
            {
                context.Remove(category);
                return null;
            }
        }

        /// <summary>
		/// Update Category object in database.
		/// </summary>
		/// <param name="request">Necessary data to save in database.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="Category">Updated Category Object.</returns> 
        public static Category UpdateCategory(CategoryRequest request, ArpaMediaContext context)
        {
            Category category = GetCategoryById(request.Id, context);
            category.Name = request.Name;
            category.ParentCategoryId = request.ParentCategoryId;
            category.DisplayOrder = request.DisplayOrder;
            category.LastModified = DateTime.Now;
            category.IsMenuItem = request.IsMenuItem;
            category.LanguageId = request.LanguageId;
            category.IsHomeItem = request.IsHomeItem;
            category.IsCalendarItem = request.IsCalendarItem;
            context.Categories.Update(category);
            context.SaveChanges();
            return category;
        }

        /// <summary>
		/// Delete Category object from database by Id with subCategories.
		/// </summary>
		/// <param name="categoryId">Id of the category to be deleted.</param>  
        /// <param name="context">Context of the Database.</param> 
        public static void DeleteCategory(int categoryId, ArpaMediaContext context)
        {

            List<Category> childCategories = context.Categories
           .Where(c => c.ParentCategoryId == categoryId)
           .Include(c => c.Language)
           .ToList();
            foreach (var eachChildCategory in childCategories)
            {
                Category childCategory = GetCategoryById(eachChildCategory.Id, context);
                context.Categories.Remove(childCategory);
            }
            Category category = GetCategoryById(categoryId, context);
            context.Categories.Remove(category);
            context.SaveChanges();
        }

        /// <summary>
		/// Get Category object from database by Id.
		/// </summary>
		/// <param name="categoryId">Id of the category to be retrieved.</param>  
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="Category">Found Category by categoryId.</returns> 
        public static Category GetCategoryById(int categoryId, ArpaMediaContext context)
        {
            var category = context.Categories
                .Where(u => u.Id == categoryId)
                .Include(c => c.InverseParentCategory)
                .Include(c => c.CategoryPosts)
                .FirstOrDefault();
            return category;
        }

        /// <summary>
        /// Get Category object from database by Name.
        /// </summary>
        /// <param name="categoryName">Name of the category to be retrieved.</param>  
        /// <param name="parentCategoryId">The id of the Parent Category if the name is for subcategory.</param>
        /// <param name="context">Context of the Database.</param> 
        public static Category GetCategoryByName(string categoryName, int? parentCategoryId, ArpaMediaContext context)
        {
            var category = context.Categories.Where(u => u.Name == categoryName && u.ParentCategoryId == parentCategoryId).FirstOrDefault();
            return category;
        }

        /// <summary>
        /// Get Categories by post id.
        /// </summary>
        /// <param name="postId">Id of the post.</param>  
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="Category">All Categories found for post id.</returns> 
        public static List<Category> GetCategoryByPostId(int postId, ArpaMediaContext context)
        {
            var query = from cp in context.CategoryPosts
                        join c in context.Categories on cp.CategoryId equals c.Id
                        where cp.PostId == postId
                        select c;
            List<Category> categories = query.ToList();
            return categories;
        }

        /// <summary>
        /// Get all categories and their subcategories.
        /// </summary>
        /// <param name="context">Context of the Database.</param>
        /// <param name="isForMenu">Check if require only menu items.</param>
        /// <returns name="Category">All Categories by categoryId.</returns> 
        public static ICollection<Category> GetCateogories(ArpaMediaContext context, bool isForMenu = false)
        {
            var categoriesHierarchy = new List<CategoryResponse>();
            var categories = context.Categories
                .Where(c => c.ParentCategoryId == null && (isForMenu ? c.IsMenuItem : true))
                .Include(c => c.Language)
                .OrderBy(c => c.DisplayOrder).ToList();
            return categories;
        }

        /// <summary>
        /// Get all children of Category.
        /// </summary>
        /// <param name="category">Category to get their children.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <param name="isForMenu">Check if require only menu items.</param>
        /// <returns name="Category">Categoriy children.</returns> 
        public static ICollection<Category> GetCategoryChildren(Category category, ArpaMediaContext context, bool isForMenu = false)
        {
            return context.Categories
                .Where(c => c.ParentCategoryId == category.Id && (isForMenu ? c.IsMenuItem : true))
                .Include(c => c.Language)
                .ToList();
        }

        /// <summary>
        /// Get all children of Category.
        /// </summary>
        /// <param name="categoryID">CategoryId to get their children.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="Categories">Child categories .</returns> 
        public static ICollection<Category> GetCategoryChildren(int categoryID, ArpaMediaContext context)
        {
            return context.Categories
                .Where(c => c.ParentCategoryId == categoryID)
                .Include(c => c.Language)
                .ToList();
        }

        /// <summary>
        /// Get Menu Categories.
        /// </summary>
        /// <param name="context">Context of the Database.</param> 
        /// <param name="languageId">Id of the Language.</param> 
        /// <returns name="List">Category Lists.</returns> 
        public static List<Category> GetHomeCategories(int languageId, ArpaMediaContext context)
        {
            List<Category> searchedCategories = context.Categories.Where(c => c.IsHomeItem == true && c.LanguageId == languageId).ToList();
            return searchedCategories;
        }
    }
}
