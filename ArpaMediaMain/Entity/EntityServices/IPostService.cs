using ArpaMedia.Web.Api.Models;
using ArpaMedia.Web.Api.Models.Response;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.EntityServices
{
    interface IPostService
    {
        /// <summary>
        /// Create Post object with all validations.
        /// </summary>
        /// <param name="request">Necessary data to save in database.</param> 
        /// <param name="configuration">Hosts settings.</param> 
        /// <returns name="ResponseBase">Saved RequestPost Object or Error object if any issues.</returns> 
        public Task<ResponseBase> CreatePost(PostRequest request, IConfiguration configuration);

        /// <summary>
        /// Update Post object with all validations.
        /// </summary>
        /// <param name="domain">Domain of the site.</param> 
        /// <param name="request">Necessary data to save in database.</param> 
        /// <returns name="ResponseBase">Updated RequestPost Object or Error object if any issues.</returns> 
        public ResponseBase UpdatePost(string domain,PostRequest request);

        /// <summary>
        /// Delete Post object from databese by Id.
        /// </summary>
        /// <param name="request">Necessary data to delete post object from database.</param> 
        /// <returns name="ResponseBase">Successes message or Error object if any issues.</returns> 
        public ResponseBase DeletePost(DeleteRequest request);

        /// <summary>
        /// Upload image for the post object.
        /// </summary>
        /// <param name="PostId">Id of the Post object to be modified.</param> 
        /// <param name="imageStream">Data of the image.</param> 
        /// <returns name="ResponseBase">Success message.</returns> 
        public ResponseBase UploadImage(int PostId, Stream imageStream);

        /// <summary>
        /// Search Post object.
        /// </summary>
        /// <param name="request">Search params.</param> 
        /// <returns name="ResponseBase">Search Post or Error object if any issues.</returns> 
        public ResponseBase SearchPost(PostSearchRequest request);

        /// <summary>
        /// Search Post object.
        /// </summary>
        /// <param name="request">Search params.</param> 
        /// <param name="domain">domain of the web site.</param> 
        /// <returns name="ResponseBase">Search Post or Error object if any issues.</returns> 
        public ResponseBase SearchPostForDomain(string domain, PostSearchRequest request);

        /// <summary>
        /// Get Post object.
        /// </summary>
        /// <param name="request">Get Posts params.</param> 
        /// <returns name="ResponseBase"> Post or Error object if any issues.</returns> 
        public ResponseBase GetPosts(PostItemRequest request);

        /// <summary>
        /// Get Post replace imageURL.
        /// </summary>
        /// <param name="request">Get Posts params.</param> 
        /// <param name="domain">Domain of the web site.</param> 
        /// <returns name="ResponseBase"> Posts or Error object if any issues.</returns> 
        public ResponseBase GetPostsWithDomain(string domain, PostItemRequest request);

        /// <summary>
        /// Get Calendar Items.
        /// </summary>
        /// <param name="request">Get Calendar Items.</param> 
        /// <returns name="ResponseBase"> Posts or Error object if any issues.</returns> 
        public ResponseBase GetCalendarItems(CalendarItemsRequest request);

        /// <summary>
        /// Get Calendar Items replace imageURL.
        /// </summary>
        /// <param name="request">Get Calendar Items.</param> 
        /// <param name="domain">Domain of the site.</param> 
        /// <returns name="ResponseBase"> Posts or Error object if any issues.</returns> 
        public ResponseBase GetCalendarItemsWithDomain(string domain, CalendarItemsRequest request);

        /// <summary>
        /// Get Menu Items.
        /// </summary>
        /// <returns name="ResponseBase"> Posts or Error object if any issues.</returns> 
        public ResponseBase GetHomeItems(HomeItemsRequest request);

        /// <summary>
        /// Get Menu Items change imageURL.
        /// </summary>
        /// <returns name="ResponseBase"> Posts or Error object if any issues.</returns> 
        public ResponseBase GetHomeItemsWithDomain(string domain, HomeItemsRequest request);
      
        /// <summary>
        /// Search Post by id.
        /// </summary>
        /// <param name="id">Id of the post params.</param> 
        /// <returns name="ResponseBase">Search Post or Error object if any issues.</returns> 
       public ResponseBase GetPostById(int id);

        /// <summary>
        /// Search Post by id , replace imageURL.
        /// </summary>
        /// <param name="id">Id of the post params.</param> 
        /// <param name="domain">Domain of the web site.</param> 
        /// <returns name="ResponseBase">Search Post or Error object if any issues.</returns> 
        public ResponseBase GetPostByIdWithDomain(string domain, int id);
    }
}
