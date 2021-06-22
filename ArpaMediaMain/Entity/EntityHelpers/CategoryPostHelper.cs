using ArpaMedia.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Entity.EntityHelpers
{
    public class CategoryPostHelper : BaseEntityHelper

    {  
        /// <summary>
        /// Get Category by PostId.
        /// </summary>
        /// <param name="categoryId">Category id.</param> 
        /// <param name="postId">Post id.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="CategoryPost">Get CategoryPost.</returns> 
        public static CategoryPost GetCategoryPost(int categoryId, int postId, ArpaMediaContext context)
        {
            CategoryPost categoryPost = context.CategoryPosts.Where(u => u.CategoryId == categoryId && u.PostId == postId).FirstOrDefault();
            return categoryPost;
        }

        /// <summary>
        /// Add CategoryPost in Database.
        /// </summary>
        /// <param name="categoryId">Category id.</param> 
        /// <param name="postId">Post id.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="CategoryPost">Save CategoryPost in Database .</returns> 
        public static CategoryPost AddCategoryPost(int categoryId, int postId, ArpaMediaContext context)
        {
            CategoryPost categoryPost = new CategoryPost();
            categoryPost.CategoryId = categoryId;
            categoryPost.PostId = postId;
            var savedPost = context.Add(categoryPost);
            context.SaveChanges();
            return savedPost.Entity;
        }

        /// <summary>
        /// Delete CategoryPost from Database.
        /// </summary>
        /// <param name="categoryPostId">CategoryPost id.</param> 
        /// <param name="context">Context of the Database.</param> 
        public static void DeleteCategoryPost(int categoryPostId, ArpaMediaContext context)
        {
            CategoryPost categoryPosts = context.CategoryPosts.Where(u => u.Id == categoryPostId).FirstOrDefault();
            context.Remove(categoryPosts);
            context.SaveChanges();
        }

        /// <summary>
        /// Get CategoryPost by PostId.
        /// </summary>
        /// <param name="postId">Post Id.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="CategoryPost">Get CategoryPost by PostId.</returns> 
        public static List<CategoryPost> GetCategoryPostsByPostId(int postId, ArpaMediaContext context)
        {
            List<CategoryPost> categoryPosts = context.CategoryPosts.Where(u => u.PostId == postId).ToList();
            return categoryPosts;
        }

    }
}
