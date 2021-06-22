using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.Entity.EntityHelpers;
using ArpaMedia.Web.Api.EntityProviders;
using ArpaMedia.Web.Api.Models;
using ArpaMedia.Web.Api.Models.Response;
using ArpaMedia.Web.Api.Services.Authorization;
using ArpaMedia.Web.Api.UploadServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.EntityHelpers
{
    public class PostHelper : BaseEntityHelper
    {
        /// <summary>
        /// Create Post object in database.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="Post">Saved Post Object.</returns> 
        public static Post CreatePost(PostRequest request, ArpaMediaContext context)
        {

            Post post = new Post();
            post.Title = request.Title;
            post.Description = request.Description;
            post.PublishedDate = request.PublishedDate;
            post.LastModified = DateTime.Now;
            post.Created = DateTime.Now;
            post.AudioFile = request.AudioFile;
            post.VideoFile = request.VideoFile;
            try
            {
                var savedPost = context.Add(post);
                context.SaveChanges();
                return savedPost.Entity;
            }
            catch
            {
                context.Remove(post);
                return null;
            }
        }

        /// <summary>
        /// Update Post object in database.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <param name="context">Context of the Database.</param> 
        /// <returns name="Post">Updated Post Object.</returns> 
        public static Post UpdatePost(PostRequest request, ArpaMediaContext context)
        {
            Post post = PostHelper.GetPostById(request.Id, context);
            post.Title = request.Title;
            post.Description = request.Description;
            post.PublishedDate = request.PublishedDate;
            post.LastModified = DateTime.Now;
            post.AudioFile = request.AudioFile;
            post.VideoFile = request.VideoFile;
            try
            {
                context.SaveChanges();
                return post;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Delete Post object from database.
        /// </summary>
        /// <param name="postId">Necessary post id to Delete from database.</param> 
        /// <param name="context">Context from the Database.</param> 
        /// <returns name="bool">True if deleted, false if error.</returns> 
        public static bool DeletePost(int postId, ArpaMediaContext context)
        {
            Post post = GetPostById(postId, context);
            context.Posts.Remove(post);
            try
            {
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get Post object from database by postId.
        /// </summary>
        /// <param name="postId">Post id.</param> 
        /// <param name="context">Post of the Database.</param> 
        /// <returns name="Post">Retrived Post Object.</returns> 
        public static Post GetPostById(int postId, ArpaMediaContext context)
        {
            Post post = context.Posts.Where(u => u.Id == postId).FirstOrDefault();
            return post;
        }

        /// <summary>
        /// Search Post object.
        /// </summary>
        /// <param name="request">Search params.</param> 
        /// <param name="context">Post of the Database.</param> 
        /// <returns name="Post">Search Posts.</returns> 
        public static List<Post> SearchPost(PostSearchRequest request, ArpaMediaContext context)
        {
            if ((request.Offset == 0 && request.Limit == 0) || request.Offset < 0 || request.Limit < 0)
            {
                return new List<Post>();
            }
            string query = request.Query ?? "";
            query = query.Trim().ToLower();
            List<Post> searchedPosts;
            if (request.CategoryId == 0 || request.CategoryId == null)
            {
                searchedPosts = context.Posts.Where(u =>
                    u.Title.ToLower().Contains(query) &&
                    u.PublishedDate <= DateTime.Now)
               .OrderByDescending(c => c.LastModified)
               .Skip(request.Offset)
               .Take(request.Limit)
               .ToList();
                return searchedPosts;
            }
            searchedPosts = context.Posts.Where(u => 
            u.Title.ToLower().Contains(query) && 
            u.CategoryPosts.Where(cu => cu.CategoryId == request.CategoryId ).ToList().Count != 0  && 
            u.PublishedDate <= DateTime.Now)
                .OrderByDescending(c => c.LastModified)
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToList();
            return searchedPosts;
        }

        /// <summary>
        /// Get Post object.
        /// </summary>
        /// <param name="request">Search params.</param> 
        /// <param name="context">Post of the Database.</param> 
        /// <returns name="Post">Search Posts.</returns> 
        public static List<Post> GetPosts(PostItemRequest request, ArpaMediaContext context)
        {
            if ((request.Offset == 0 && request.Limit == 0) || request.Offset < 0 || request.Limit < 0)
            {
                return new List<Post>();
            }

            List<Post> searchedPosts;
            searchedPosts = context.Posts.Where(u =>
            u.CategoryPosts.Where(cu =>
            cu.CategoryId == request.CategoryId).ToList().Count != 0 &&
            u.PublishedDate <= DateTime.Now)
                .OrderByDescending(c => c.LastModified)
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToList();
            return searchedPosts;
        }

        /// <summary>
        /// Get Calendar Items.
        /// </summary>
        /// <param name="request">Get Calendar Items.</param> 
        /// <param name="context">Post of the Database.</param> 
        /// <param name="languageId">Id of the Language.</param> 
        /// <returns name="ResponseBase"> Posts or Error object if any issues.</returns> 
        public static List<Post> GetCalendarItems(CalendarItemsRequest request, int languageId, ArpaMediaContext context)
        { 
            List<Post> searchedPosts;
            searchedPosts = context.Posts.Where(u =>
            u.CategoryPosts.Where(cu =>
            cu.Category.IsCalendarItem == true &&
            cu.Category.LanguageId == languageId).ToList().Count != 0 &&
            u.PublishedDate <= DateTime.Now && (u.PublishedDate >= request.Start && u.PublishedDate <= request.End))
                .OrderByDescending(c => c.LastModified)
                .ToList();
            return searchedPosts;
        }

        /// <summary>
        /// Get Posts by category id and limit
        /// </summary>
        /// <param name="categoryId">Category of the posts</param> 
        /// <param name="limit">Number of posts to be retrieved.</param> 
        /// <param name="context">Post of the Database.</param> 
        /// <returns name="ResponseBase"> Posts or Error object if any issues.</returns> 
        public static List<Post> GetPostsByCategoryIdAndLimit(int categoryId, int limit, ArpaMediaContext context)
        {

            if (limit < 0)
            {
                return new List<Post>();
            }
            List<Post> searchedPosts;
            searchedPosts = context.Posts.Where(u =>
            u.CategoryPosts.Where(cu =>
            cu.Category.Id == categoryId).ToList().Count != 0 &&
            u.PublishedDate <= DateTime.Now)
                .OrderByDescending(c => c.LastModified)
                .Take(limit)
                .ToList();
            return searchedPosts;
        }
    }

}
