using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Models
{
    public class PostRequest
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
        [Url]
        public string AudioFile { get; set; }
        [Url]
        public string VideoFile { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
