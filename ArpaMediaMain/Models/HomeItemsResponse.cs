using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Models
{
    public class HomeItemsResponse
    {
        public CategoryResponse Category { get; set; }
        public List<PostResponse> Posts { get; set; }

        public HomeItemsResponse()
        {
            this.Posts = new List<PostResponse>();
        }
    }
}
