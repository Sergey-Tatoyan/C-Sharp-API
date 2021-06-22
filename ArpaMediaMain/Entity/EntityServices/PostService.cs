using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.Entity.EntityHelpers;
using ArpaMedia.Web.Api.Entity.EntityServices;
using ArpaMedia.Web.Api.EntityHelpers;
using ArpaMedia.Web.Api.EntityProviders;
using ArpaMedia.Web.Api.ErrorCodes;
using ArpaMedia.Web.Api.Models;
using ArpaMedia.Web.Api.Models.Response;
using ArpaMedia.Web.Api.Services.Authorization;
using ArpaMedia.Web.Api.UploadServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.EntityServices
{
    public class PostService : BaseEntityService, IPostService
    {
        /// <summary>
        /// Create Post object with all validations.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <param name="configuration">Configuration Settings</param> 
        /// <returns name="ResponseBase">Saved RequestPost Object or Error object if any issues.</returns> 
        public async Task<ResponseBase> CreatePost(PostRequest request, IConfiguration configuration)
        {
            try
            {
                Post postEntity = null;
                if (request.CategoryIds != null && request.CategoryIds.Count > 0)
                {
                    foreach (var categoryId in request.CategoryIds)
                    {
                        Category category = CategoryHelper.GetCategoryById(categoryId, DBArpaContext);
                        if (category != null)
                        {
                            if (postEntity == null)
                            {
                                postEntity = PostHelper.CreatePost(request, DBArpaContext);
                            }
                            CategoryPost categoryPost = CategoryPostHelper.GetCategoryPost(categoryId, postEntity.Id, DBArpaContext);
                            if (categoryPost == null)
                            {
                                CategoryPostHelper.AddCategoryPost(categoryId, postEntity.Id, DBArpaContext);
                            }
                        }
                    }
                }
                if (postEntity == null)
                {
                    BadResponse badResponse = new BadResponse("Wrong Category List");
                    return badResponse;
                }
                PostResponse postResponse = new PostResponse(postEntity, DBArpaContext);
                OkResponse<PostResponse> okResponse = new OkResponse<PostResponse>();
                okResponse.Response = postResponse;
                okResponse.StatusCode = StatusCodes.Status201Created;
                MailProvider mailProvider = new MailProvider();
                await mailProvider.SendEmailsToSubscribersForPost(postResponse, configuration);
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
        /// Update Post object with all validations.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <param name="domain">Domain of the site.</param> 
        /// <returns name="ResponseBase">Updated RequestPost Object or Error object if any issues.</returns> 
        public ResponseBase UpdatePost(string domain,PostRequest request)
        {
            Post post = PostHelper.GetPostById(request.Id, DBArpaContext);
            if (post == null)
            {
                BadResponse badResponse = new BadResponse();
                badResponse.AddResponseError("Id", new string[] { "PostId is not correct." });
                return badResponse;
            }
            Post updatedPost = PostHelper.UpdatePost(request, DBArpaContext);
            if (updatedPost == null)
            {
                BadResponse badResponse = new BadResponse();
                badResponse.AddResponseError(new string[] { "Post update is not successful." });
                return badResponse;
            }
            List<CategoryPost> categoryPostList = CategoryPostHelper.GetCategoryPostsByPostId(updatedPost.Id, DBArpaContext);
            foreach (var categorypost in categoryPostList)
            {
                CategoryPostHelper.DeleteCategoryPost(categorypost.Id, DBArpaContext);
            }

            if (request.CategoryIds != null && request.CategoryIds.Count > 0)
            {
                foreach (int categoryId in request.CategoryIds)
                {
                    Category category = CategoryHelper.GetCategoryById(categoryId, DBArpaContext);
                    if (category != null)
                    {
                        CategoryPost categoryPost = CategoryPostHelper.GetCategoryPost(categoryId, updatedPost.Id, DBArpaContext);
                        if (categoryPost == null)
                        {
                            CategoryPostHelper.AddCategoryPost(categoryId, updatedPost.Id, DBArpaContext);
                        }
                    }
                }
            }
            PostResponse postResponse = new PostResponse(post, DBArpaContext);
            postResponse.PostImage = domain + postResponse.PostImage;
            OkResponse<PostResponse> okResponse = new OkResponse<PostResponse>();
            okResponse.Response = postResponse;
            return okResponse;
        }

        /// <summary>
        /// Delete Post object from databese by Id.
        /// </summary>
        /// <param name="request">Necessary data to delete post object from database.</param> 
        /// <returns name="ResponseBase">Successes message or Error object if any issues.</returns> 
        public ResponseBase DeletePost(DeleteRequest request)
        {
            Post post = PostHelper.GetPostById(request.Id, DBArpaContext);
            if (post == null)
            {
                BadResponse badResponse = new BadResponse();
                badResponse.AddResponseError("Id", new string[] { "PostId is not correct" });
                return badResponse;
            }

            if (post.PostImage != null)
            {
                ImageProvider imageProvider = new ImageProvider();
                imageProvider.DeleteFile(post.PostImage);
            }

            List<CategoryPost> categoryPostList = CategoryPostHelper.GetCategoryPostsByPostId(request.Id, DBArpaContext);
            foreach (var categorypost in categoryPostList)
            {
                CategoryPostHelper.DeleteCategoryPost(categorypost.Id, DBArpaContext);
            }

            bool isDeleted = PostHelper.DeletePost(request.Id, DBArpaContext);
            if (!isDeleted)
            {
                BadResponse badResponse = new BadResponse();
                badResponse.AddResponseError(new string[] { "Delete is not successful." });
                return badResponse;
            }

            OkResponse<string> okResponse = new OkResponse<string>();
            okResponse.Response = "Success";
            return okResponse;
        }

        /// <summary>
        /// Upload image for the post object.
        /// </summary>
        /// <param name="PostId">Id of the Post object to be modified.</param> 
        /// <param name="imageStream">Data of the image.</param> 
        /// <returns name="ResponseBase">Success message.</returns> 
        public ResponseBase UploadImage(int PostId, Stream imageStream)
        {
            if (PostId <= 0)
            {
                return new BadResponse("Incorrect Post Id.");
            }

            Post post = PostHelper.GetPostById(PostId, DBArpaContext);
            if (post == null)
            {
                return new BadResponse("Incorrect Post Id.");
            }

            ImageProvider imageProvider = new ImageProvider();
            try
            {
                if (post.PostImage != null)
                {
                    imageProvider.DeleteFile(post.PostImage);
                }

                string imagePath = imageProvider.UploadImage(imageStream);
                post.PostImage = imagePath;

                PostHelper.SaveContext(DBArpaContext);

                OkResponse<string> okResponse = new OkResponse<string>();
                okResponse.Response = "Success";
                okResponse.StatusCode = StatusCodes.Status201Created;
                return okResponse;
            }
            catch (Exception e)
            {
                BadResponse badResponse = new BadResponse();
                badResponse.AddResponseError(e.Message);
                return badResponse;
            }
        }

        /// <summary>
        /// Search Post object.
        /// </summary>
        /// <param name="request">Search params.</param> 
        /// <returns name="ResponseBase">Search Post or Error object if any issues.</returns> 
        public ResponseBase SearchPost(PostSearchRequest request)
        {
            List<Post> searchedPosts = PostHelper.SearchPost(request, DBArpaContext);
            List<PostResponse> postResponseList = new List<PostResponse>();
            foreach (Post post in searchedPosts)
            {
                PostResponse postResponse = new PostResponse(post, DBArpaContext);
                postResponseList.Add(postResponse);
            }

            OkResponse<List<PostResponse>> okResponse = new OkResponse<List<PostResponse>>();
            okResponse.Response = postResponseList;
            return okResponse;
        }

        /// <summary>
        /// Search Post by id.
        /// </summary>
        /// <param name="id">Id of the post params.</param> 
        /// <returns name="ResponseBase">Search Post or Error object if any issues.</returns> 
        public ResponseBase GetPostById(int id)
        {
            Post post = PostHelper.GetPostById(id, DBArpaContext);
            if (post == null)
            {
                BadResponse badResponse = new BadResponse("Post with this Id not found.");
                return badResponse;
            }
            PostResponse postResponse = new PostResponse(post, DBArpaContext);
            OkResponse<PostResponse> okResponse = new OkResponse<PostResponse>();
            okResponse.Response = postResponse;
            return okResponse;
        }
        /// <summary>
        /// Search Post by id, replace imageURL.
        /// </summary>
        /// <param name="id">Id of the post params.</param> 
        /// <param name="domain">Domain of the web site.</param> 
        /// <returns name="ResponseBase">Search Post or Error object if any issues.</returns> 
        public ResponseBase GetPostByIdWithDomain(string domain, int id)
        {
            ResponseBase searchResponse = GetPostById(id);
            if (searchResponse.StatusCode == StatusCodes.Status200OK)
            {
                OkResponse<PostResponse> successRespnse = (OkResponse<PostResponse>)searchResponse;
                PostResponse postResponses = successRespnse.Response;
               postResponses.PostImage = domain + postResponses.PostImage;
                
            }
            return searchResponse;
        }
        /// <summary>
        /// SearchPostForDomain
        /// </summary>
        /// <param name="domain">Domain of the web site.</param> 
        /// <param name="request">Search params.</param> 
        /// <returns name="ResponseBase">Search Post or Error object if any issues.</returns>
        public ResponseBase SearchPostForDomain(string domain, PostSearchRequest request)
        {
            ResponseBase searchResponse = SearchPost(request);
            if (searchResponse.StatusCode == StatusCodes.Status200OK)
            {
                OkResponse<List<PostResponse>> successRespnse = (OkResponse<List<PostResponse>>)searchResponse;
                List<PostResponse> postResponses = successRespnse.Response;
                foreach (var eachePostResponse in postResponses)
                {
                    eachePostResponse.PostImage = domain + eachePostResponse.PostImage;
                }
            }
            return searchResponse;
        }

        /// <summary>
        /// Get Post object.
        /// </summary>
        /// <param name="request">Get Posts params.</param> 
        /// <returns name="ResponseBase"> Post or Error object if any issues.</returns> 
        public ResponseBase GetPosts(PostItemRequest request)
        {
            List<Post> searchedPosts = PostHelper.GetPosts(request, DBArpaContext);
            List<PostResponse> postResponseList = new List<PostResponse>();
            foreach (Post post in searchedPosts)
            {
                PostResponse postResponse = new PostResponse(post, DBArpaContext);
                postResponseList.Add(postResponse);
            }

            OkResponse<List<PostResponse>> okResponse = new OkResponse<List<PostResponse>>();
            okResponse.Response = postResponseList;
            return okResponse;
        }

        /// <summary>
        /// Get Posts replace imageURL.
        /// </summary>
        /// <param name="request">Get Posts params.</param> 
        /// <param name="domain">Domain of the web site.</param> 
        /// <returns name="ResponseBase"> Post or Error object if any issues.</returns> 
        public ResponseBase GetPostsWithDomain(string domain, PostItemRequest request)
        {
            ResponseBase searchResponse = GetPosts(request);
            if (searchResponse.StatusCode == StatusCodes.Status200OK)
            {
                OkResponse<List<PostResponse>> successRespnse = (OkResponse<List<PostResponse>>)searchResponse;
                List<PostResponse> postResponses = successRespnse.Response;
                foreach (var eachePostResponse in successRespnse.Response)
                {
                    eachePostResponse.PostImage = domain + eachePostResponse.PostImage;
                }
            }
            return searchResponse;
        }

        /// <summary>
        /// Get Calendar Items.
        /// </summary>
        /// <param name="request">Get Calendar Items.</param> 
        /// <returns name="ResponseBase"> Posts or Error object if any issues.</returns> 
        public ResponseBase GetCalendarItems(CalendarItemsRequest request)
        {
            var language = LanguageHelper.GetLanguageByCode(request.Language, DBArpaContext);
            if (language == null)
            {
                BadResponse badResponse = new BadResponse();
                badResponse.AddResponseError(LanguageErrors.WrongLanguage);
                return badResponse;
            }
            List<Post> searchedPosts = PostHelper.GetCalendarItems(request, language.Id, DBArpaContext);
            List<PostResponse> postResponseList = new List<PostResponse>();
            foreach (Post post in searchedPosts)
            {
                PostResponse postResponse = new PostResponse(post, DBArpaContext);
                postResponseList.Add(postResponse);
            }

            OkResponse<List<PostResponse>> okResponse = new OkResponse<List<PostResponse>>();
            okResponse.Response = postResponseList;
            return okResponse;
        }

        /// <summary>
        /// Get Calendar Items, replace imageURL.
        /// </summary>
        /// <param name="request">Get Calendar Items.</param> 
        /// <param name="domain">Domain of the site.</param> 
        /// <returns name="ResponseBase"> Posts or Error object if any issues.</returns> 
        public ResponseBase GetCalendarItemsWithDomain(string domain, CalendarItemsRequest request)
        {
            ResponseBase searchResponse = GetCalendarItems(request);
            if (searchResponse.StatusCode == StatusCodes.Status200OK)
            {
                OkResponse<List<PostResponse>> successRespnse = (OkResponse<List<PostResponse>>)searchResponse;
                List<PostResponse> postResponses = successRespnse.Response;
                foreach (var eachePostResponse in successRespnse.Response)
                {
                    eachePostResponse.PostImage = domain + eachePostResponse.PostImage;
                }
            }
            return searchResponse;
        }

        /// <summary>
        /// Get Menu Items.
        /// </summary>
        /// <returns name="ResponseBase"> Posts or Error object if any issues.</returns> 
        public ResponseBase GetHomeItems(HomeItemsRequest request)
        {
            var language = LanguageHelper.GetLanguageByCode(request.Language, DBArpaContext);
            if (language == null)
            {
                BadResponse badResponse = new BadResponse();
                badResponse.AddResponseError(LanguageErrors.WrongLanguage);
                return badResponse;
            }
            List<Category> categories = CategoryHelper.GetHomeCategories(language.Id, DBArpaContext);
            List<HomeItemsResponse> responses = new List<HomeItemsResponse>();
            foreach (Category item in categories)
            {
                HomeItemsResponse menuItemsResponse = new HomeItemsResponse();
                menuItemsResponse.Category = new CategoryResponse(item);
                List<Post> posts = PostHelper.GetPostsByCategoryIdAndLimit(item.Id, request.PostsLimit, DBArpaContext);
                foreach (var post in posts)
                {
                    var postResponse = new PostResponse(post, DBArpaContext);
                    menuItemsResponse.Posts.Add(postResponse);
                }

                responses.Add(menuItemsResponse);
            }

            OkResponse<List<HomeItemsResponse>> okResponse = new OkResponse<List<HomeItemsResponse>>();
            okResponse.Response = responses;
            return okResponse;
        }

        /// <summary>
        /// Get Menu Items replace imageURL.
        /// </summary>
        /// <returns name="ResponseBase"> Posts or Error object if any issues.</returns> 
        public ResponseBase GetHomeItemsWithDomain(string domain, HomeItemsRequest request)
        {
            ResponseBase searchResponse = GetHomeItems(request);
            if (searchResponse.StatusCode == StatusCodes.Status200OK)
            {
                OkResponse<List<PostResponse>> successRespnse = (OkResponse<List<PostResponse>>)searchResponse;
                List<PostResponse> postResponses = successRespnse.Response;
                foreach (var eachePostResponse in successRespnse.Response)
                {
                    eachePostResponse.PostImage = domain + eachePostResponse.PostImage;
                }
            }
            return searchResponse;
        }
    }
}
