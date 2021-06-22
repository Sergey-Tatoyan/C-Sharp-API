using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Models
{
    public class BannerRequest
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        public string SmallText { get; set; }
   /*     public string ImageUrl { get; set; }*/
        [Required]
        public int BannerTypeId { get; set; }
    }
}
