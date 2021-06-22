using ArpaMedia.Data.Models;
using ArpaMedia.Web.Api.EntityProviders;
using System;
using System.Collections.Generic;

namespace ArpaMedia.Web.Api.Models
{
    public class PostResponse
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }
        public string PostImage { get; set; }
        public string AudioFile { get; set; }
        public string VideoFile { get; set; }
        public List<CategoryResponse> Categories { get; set; }

        public PostResponse(Post post, ArpaMediaContext context)
        {
            this.Id = post.Id;
            this.Title = post.Title;
            this.Description = post.Description;
            this.PublishedDate = post.PublishedDate;
            this.PostImage = post.PostImage;
            this.AudioFile = post.AudioFile;
            this.VideoFile = post.VideoFile;
            List<Category> filtredCategories = CategoryHelper.GetCategoryByPostId(post.Id, context);
            this.Categories = new List<CategoryResponse>();

            foreach (var category in filtredCategories)
            {
                CategoryResponse categoryResponse = new CategoryResponse(category);
                this.Categories.Add(categoryResponse);
            }
        }

        public PostResponse()
        {

        }
    }
}
