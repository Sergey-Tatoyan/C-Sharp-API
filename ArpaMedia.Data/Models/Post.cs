using System;
using System.Collections.Generic;

#nullable disable

namespace ArpaMedia.Data.Models
{
    public partial class Post
    {
        public Post()
        {
            CategoryPosts = new HashSet<CategoryPost>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string PostImage { get; set; }
        public string AudioFile { get; set; }
        public string VideoFile { get; set; }

        public virtual ICollection<CategoryPost> CategoryPosts { get; set; }
    }
}
